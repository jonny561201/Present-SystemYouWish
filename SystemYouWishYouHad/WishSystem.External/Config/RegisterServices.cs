using Microsoft.Extensions.DependencyInjection;

namespace WishSystem.External.Config;

public static class RegisterServices
{
    public static IServiceCollection AddExternalClients(this IServiceCollection services)
    {
        services.AddHttpClient<IExternalClient>(c =>
        {
            c.BaseAddress = new Uri("https://postman-echo.com/");
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddTransient<IExternalClient, ExternalClient>();
        
        return services;
    }
}