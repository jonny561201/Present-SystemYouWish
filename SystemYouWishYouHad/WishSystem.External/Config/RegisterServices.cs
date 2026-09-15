using Microsoft.Extensions.DependencyInjection;

namespace WishSystem.External.Config;

public static class RegisterServices
{
    public static IServiceCollection AddExternalClients(this IServiceCollection services)
    {
        services.AddHttpClient<IExternalClient, ExternalClient>(c =>
        {
            c.BaseAddress = new Uri("https://postman-echo.com");
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        return services;
    }
}