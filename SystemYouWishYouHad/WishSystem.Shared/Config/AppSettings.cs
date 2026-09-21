namespace WishSystem.Shared.Config;

public record AppSettings
{
    public required string Environment { get; init; }
    public string[] AllowedOrigins { get; init; } = [];
    public required UserDatabase UserDatabase { get; init; }
    public required BaseEndPoints BaseEndPoints { get; init; }
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

public record BaseEndPoints
{
    public required string Submit { get; init; }
}

public record Sqs
{
    public required string ServiceUrl { get; init; }
    public required string QueueName { get; init; }
}
