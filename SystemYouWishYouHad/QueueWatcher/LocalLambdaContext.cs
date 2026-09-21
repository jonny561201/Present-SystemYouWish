using Amazon.Lambda.Core;

namespace QueueWatcher;

/// <summary>
/// Stand-in for the ILambdaContext that AWS supplies at runtime. None of this is
/// meaningful locally, it just has to be non-null for the handler to run.
/// </summary>
public class LocalLambdaContext : ILambdaContext
{
    public string AwsRequestId { get; } = Guid.NewGuid().ToString();
    public IClientContext ClientContext => null!;
    public string FunctionName => "WishSystem.Lambda";
    public string FunctionVersion => "$LATEST";
    public ICognitoIdentity Identity => null!;
    public string InvokedFunctionArn => "arn:aws:lambda:us-east-1:000000000000:function:WishSystem.Lambda";
    public ILambdaLogger Logger { get; } = new ConsoleLambdaLogger();
    public string LogGroupName => "/aws/lambda/WishSystem.Lambda";
    public string LogStreamName => "local";
    public int MemoryLimitInMB => 512;
    public TimeSpan RemainingTime => TimeSpan.FromSeconds(30);
}

public class ConsoleLambdaLogger : ILambdaLogger
{
    public void Log(string message) => Console.Write(message);

    public void LogLine(string message) => Console.WriteLine(message);
}
