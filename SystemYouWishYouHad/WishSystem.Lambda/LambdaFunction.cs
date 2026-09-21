using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WishSystem.External;
using WishSystem.External.Config;
using WishSystem.Shared.Config;

namespace WishSystem.Lambda;

public class LambdaFunction
{
    private readonly IExternalClient _client;
    
    public LambdaFunction()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var settings = config.Get<AppSettings>();

        _client = new ServiceCollection()
            .AddExternalHttpClients(settings)
            .BuildServiceProvider()
            .GetRequiredService<IExternalClient>();
    }

    public async Task Handler(SQSEvent e, ILambdaContext context)
    {
        foreach (var record in e.Records)
        {
            var users = JsonSerializer.Deserialize<List<Guid>>(record.Body);

            if (users is null or { Count: 0 })
                continue;

            await _client.Submit(users);
        }
    }
}
