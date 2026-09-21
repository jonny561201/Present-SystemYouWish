using WishSystem.API.Services;

namespace WishSystem.API.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/users", (IUserService service) => Results.Ok(service.GetUsers()));

        app.MapPost("/users/submit", (IUserService service, List<Guid> userId) => service.SubmitUsers(userId));

        return app;
    }
}