namespace SystemAPI.Config;

public record AppSettings
{
    public string Environment { get; init; }
    public string[] AllowedOrigins { get; init; }
}