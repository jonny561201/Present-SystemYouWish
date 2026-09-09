using System.Shared.Config;

namespace System.API.Config;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfig (this IServiceCollection services, AppSettings appSettings)
    {
        services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policy => 
            { 
                    policy.WithOrigins(appSettings.AllowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod(); 
            }));
        
        return services;
    }
}