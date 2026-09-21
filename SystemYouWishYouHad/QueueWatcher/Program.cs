using Amazon.Lambda.SQSEvents;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using QueueWatcher;
using WishSystem.Lambda;

const string serviceUrl = "http://localhost:9324";
const string queueName = "user-submit";
const string region = "us-east-1";
const int maxAttempts = 5;

var cancellation = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cancellation.Cancel();
};

// ElasticMQ ignores credentials, but the SDK will not sign a request without some
// credential resolving - and the default chain would otherwise pick up a real AWS profile.
var sqs = new AmazonSQSClient(
    new BasicAWSCredentials("local", "local"),
    new AmazonSQSConfig
    {
        ServiceURL = serviceUrl,
        AuthenticationRegion = region,
    });

// CreateQueue is idempotent, so this both creates the queue on a fresh ElasticMQ
// and resolves the url on a warm one.
var queueUrl = (await sqs.CreateQueueAsync(queueName, cancellation.Token)).QueueUrl;
var function = new LambdaFunction();

Console.WriteLine($"Watching {queueUrl} -> WishSystem.Lambda::LambdaFunction::Handler");
Console.WriteLine("Press Ctrl-C to stop.");

while (!cancellation.IsCancellationRequested)
{
    try
    {
        var received = await sqs.ReceiveMessageAsync(new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 10,
            WaitTimeSeconds = 20,
            VisibilityTimeout = 30,
            MessageSystemAttributeNames = ["All"],
            MessageAttributeNames = ["All"],
        }, cancellation.Token);

        if (received.Messages is not { Count: > 0 })
            continue;

        // Drop anything that has already failed too many times, otherwise a throwing
        // handler would redeliver the same message forever. A real queue uses a DLQ.
        var exhausted = received.Messages.Where(IsExhausted).ToList();
        foreach (var message in exhausted)
            Console.WriteLine($"Giving up on {message.MessageId} after {maxAttempts} attempts: {message.Body}");

        var messages = received.Messages.Except(exhausted).ToList();
        await Delete(exhausted);

        if (messages.Count == 0)
            continue;

        // A real event source mapping invokes once per batch, not once per message.
        var sqsEvent = new SQSEvent { Records = messages.Select(ToRecord).ToList() };

        Console.WriteLine($"Invoking handler with {messages.Count} message(s).");

        try
        {
            function.Handler(sqsEvent, new LocalLambdaContext());
        }
        catch (Exception ex)
        {
            // Leave the batch on the queue - it becomes visible again after the
            // visibility timeout and is redelivered, same as real SQS.
            Console.WriteLine($"Handler threw, leaving {messages.Count} message(s) for redelivery: {ex}");
            continue;
        }

        await Delete(messages);
    }
    catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
    {
        break;
    }
    catch (Exception ex)
    {
        // Keep watching if ElasticMQ restarts mid-demo.
        Console.WriteLine($"Queue error: {ex.Message}");
        await Task.Delay(TimeSpan.FromSeconds(2), CancellationToken.None);
    }
}

Console.WriteLine("Stopped.");
return;

bool IsExhausted(Message message) =>
    message.Attributes is not null
    && message.Attributes.TryGetValue("ApproximateReceiveCount", out var count)
    && int.TryParse(count, out var attempts)
    && attempts > maxAttempts;

async Task Delete(IReadOnlyCollection<Message> batch)
{
    if (batch.Count == 0)
        return;

    await sqs.DeleteMessageBatchAsync(new DeleteMessageBatchRequest
    {
        QueueUrl = queueUrl,
        Entries = batch
            .Select(m => new DeleteMessageBatchRequestEntry(m.MessageId, m.ReceiptHandle))
            .ToList(),
    }, CancellationToken.None);
}

SQSEvent.SQSMessage ToRecord(Message message) => new()
{
    MessageId = message.MessageId,
    ReceiptHandle = message.ReceiptHandle,
    Body = message.Body,
    Md5OfBody = message.MD5OfBody,
    Attributes = message.Attributes ?? new Dictionary<string, string>(),
    EventSource = "aws:sqs",
    EventSourceArn = $"arn:aws:sqs:{region}:000000000000:{queueName}",
    AwsRegion = region,
};
