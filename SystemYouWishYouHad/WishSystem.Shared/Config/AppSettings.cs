namespace WishSystem.Shared.Config;

public record AppSettings
{
    public string Environment { get; init; }
    public string[] AllowedOrigins { get; init; }
    public UserDatabase UserDatabase { get; init; } = new UserDatabase();
}

public record UserDatabase
{
    public string Host { get; init; }
    public string Name { get; init; }
    public int Port { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}