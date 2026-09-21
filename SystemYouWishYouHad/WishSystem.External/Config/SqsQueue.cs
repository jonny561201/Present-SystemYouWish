using Amazon.SQS;

namespace WishSystem.External.Config;

public record SqsQueue(IAmazonSQS Client, string Url);
