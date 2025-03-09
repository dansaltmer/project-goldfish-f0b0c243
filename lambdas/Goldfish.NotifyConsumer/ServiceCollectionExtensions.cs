using Goldfish.DynamoDb.Connections.Configuration;
using Goldfish.DynamoDb.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goldfish.NotifyConsumer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotifyConsumerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionsConfiguration = configuration.GetSection("Connections").Get<DynamoDbConnectionsConfiguration>()!;
        services.AddDynamoDbConnectionsStore(connectionsConfiguration);

        return services;
    }
}
