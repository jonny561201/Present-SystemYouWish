namespace System.API.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        app.MapGet("/users", () => "Hello World!");
        
        app.MapPost("/users", () => "Hello World!");
        
        return app;
    }
}