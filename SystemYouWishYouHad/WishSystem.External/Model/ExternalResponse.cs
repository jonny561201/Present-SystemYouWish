namespace WishSystem.External.Model;

public record Headers(string Host);

public record ExternalResponse(
    List<Guid> Data,
    Headers Headers
);