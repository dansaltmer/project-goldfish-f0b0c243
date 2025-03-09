using Amazon;
using Amazon.DynamoDBv2;
using Goldfish.Domain.Services;
using Goldfish.DynamoDb.Connections.Configuration;
using Goldfish.DynamoDb.Connections.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Goldfish.DynamoDb.Connections;

public static class ServiceCollectionExtensions
{
    public const string CONNECTIONS_KEY = nameof(CONNECTIONS_KEY);

    public static IServiceCollection AddDynamoDbConnectionsStore(this IServiceCollection services, DynamoDbConnectionsConfiguration configuration)
    {
        services.AddKeyedSingleton(CONNECTIONS_KEY, new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            RegionEndpoint = RegionEndpoint.EUNorth1
        }));

        services.AddTransient<IConnectionStore>(sp =>
        {
            var client = sp.GetRequiredKeyedService<AmazonDynamoDBClient>(CONNECTIONS_KEY);
            return new DynamoDbConnectionStore(configuration, client);
        });

        return services;
    }
}
