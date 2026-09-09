using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using WishSystem.Shared.Config;

namespace WishSystem.Data.Config;

public static class SystemYouWishConfig
{
    public static IServiceCollection AddUserDbContext(this IServiceCollection services, AppSettings settings)
    {
        var test = new NpgsqlConnectionStringBuilder
        {
            Host = settings.UserDatabase.Host, 
            Port = settings.UserDatabase.Port,
            Database = settings.UserDatabase.Name,
            Username = settings.UserDatabase.Username,
            Password = settings.UserDatabase.Password,
        };
        Console.WriteLine(test.ToString());
        
        services.AddDbContext<SystemYouWishContext>(options => options.UseNpgsql(test.ConnectionString));
        
        return services;
    }
}