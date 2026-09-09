using WishSystem.API.Services;

namespace WishSystem.API.Config;

public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        
        return services;
    }
}