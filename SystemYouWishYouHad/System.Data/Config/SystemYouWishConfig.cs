using System.Shared.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace System.Data.Config;

public static class SystemYouWishDbConfig
{
    public static IServiceCollection ConfigUserDb(this IServiceCollection services, AppSettings settings)
    {
        var test = new NpgsqlConnectionStringBuilder
        {
            Host = settings.UsersDb.Host, 
            Port = settings.UsersDb.Port,
            Database = settings.UsersDb.Name,
            Username = settings.UsersDb.Username,
            Password = settings.UsersDb.Password,
        };
        
        services.AddDbContext<SystemYouWishContext>(options => options.UseNpgsql(test.ConnectionString));
        
        return services;
    }
}