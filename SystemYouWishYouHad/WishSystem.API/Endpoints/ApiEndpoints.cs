using WishSystem.API.Services;

namespace WishSystem.API.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/users", (IUserService service) => service.GetUsers());
        
        app.MapGet("/users/{id:guid}", (IUserService service, Guid id) => service.GetUser(id));
        
        app.MapPost("/users/submit", (IUserService service, List<Guid> userId) => service.SubmitUsers(userId));
        
        return app;
    }
}