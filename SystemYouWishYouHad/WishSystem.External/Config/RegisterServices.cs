using Microsoft.Extensions.DependencyInjection;
using WishSystem.Shared.Config;

namespace WishSystem.External.Config;

public static class RegisterServices
{
    public static IServiceCollection AddExternalClients(this IServiceCollection services, AppSettings settings)
    {
        services.AddHttpClient<IExternalClient, ExternalClient>(c =>
        {
            c.BaseAddress = new Uri(settings.BaseEndpoints.Submit);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        return services;
    }
}