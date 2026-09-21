namespace WishSystem.Shared.Config;

public record AppSettings
{
    public required UserDatabase UserDatabase { get; init; }
    public required BaseEndpoints BaseEndpoints { get; init; }
    public required Sqs Sqs { get; init; }
}

public record UserDatabase
{
    public required string Host { get; init; }
    public required string Name { get; init; }
    public required int Port { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public record BaseEndpoints
{
    public required string Submit { get; init; }
}

public record Sqs
{
    public required string ServiceUrl { get; init; }
    public required string QueueUrl { get; init; }
}
