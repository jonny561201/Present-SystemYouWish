using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using WishSystem.Shared.Config;

namespace WishSystem.External.Config;

public static class RegisterServices
{
    public static IServiceCollection AddExternalHttpClients(this IServiceCollection services, AppSettings settings)
    {
        services.AddHttpClient<IExternalClient, ExternalClient>(c =>
        {
            c.BaseAddress = new Uri(settings.BaseEndpoints.Submit);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        return services;
    }

    public static IServiceCollection AddExternalSqsClients(this IServiceCollection services, AppSettings settings)
    {
        services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(
            new BasicAWSCredentials("local", "local"),
            new AmazonSQSConfig
            {
                ServiceURL = settings.Sqs.ServiceUrl,
                AuthenticationRegion = "us-east-1",
            }));

        services.AddSingleton(sp => new SqsQueue(sp.GetRequiredService<IAmazonSQS>(), settings.Sqs.QueueUrl));

        services.AddTransient<IExternalClient, SqsExternalClient>();

        return services;
    }
}