namespace System.API.Models;

public record UserResponse
{
    public string GivenName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string FamilyName { get; set; } = null!;
    public string Email { get; set; } = null!;
}