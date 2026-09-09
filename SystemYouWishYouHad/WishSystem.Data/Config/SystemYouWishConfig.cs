using System.Shared.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace System.Data.Config;

public static class SystemYouWishConfig
{
    public static IServiceCollection AddUserDbContext(this IServiceCollection services, AppSettings settings)
    {
        var test = new NpgsqlConnectionStringBuilder
        {
            Host = settings.UsersDb.Host, 
            Port = settings.UsersDb.Port,
            Database = settings.UsersDb.Name,
            Username = settings.UsersDb.Username,
            Password = settings.UsersDb.Password,
        };
        Console.WriteLine(test.ToString());
        
        services.AddDbContext<SystemYouWishContext>(options => options.UseNpgsql(test.ConnectionString));
        
        return services;
    }
}