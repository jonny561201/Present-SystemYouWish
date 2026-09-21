using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using WishSystem.Shared.Config;

namespace WishSystem.External.Config;

public static class RegisterServices
{
    public static IServiceCollection AddExternalClients(this IServiceCollection services, AppSettings settings)
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
