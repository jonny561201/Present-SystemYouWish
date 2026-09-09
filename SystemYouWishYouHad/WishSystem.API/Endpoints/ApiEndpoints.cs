using System.API.Services;

namespace System.API.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/users/{id:guid}", (IUserService service, Guid id) =>
        {
            return service.GetUser(id);
        });
        
        app.MapPost("/users/submit", (IUserService service, List<Guid> userId) =>
        {
            return service.SubmitUsers(userId);
        });
        
        return app;
    }
}