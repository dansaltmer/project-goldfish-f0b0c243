using Amazon;
using Amazon.DynamoDBv2;
using Goldfish.Domain.Services;
using Goldfish.DynamoDb.Messages.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Goldfish.DynamoDb.Messages;

public static class ServiceCollectionExtensions
{
    public const string MESSAGES_KEY = nameof(MESSAGES_KEY);

    public static IServiceCollection AddDynamoDbMessageStore(
        this IServiceCollection services, 
        DynamoDbMessagesConfiguration configuration)
    {
        services.AddKeyedSingleton(MESSAGES_KEY, new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            RegionEndpoint = RegionEndpoint.EUNorth1
        }));

        services.AddTransient<IMessageStore>(sp =>
        {
            var client = sp.GetRequiredKeyedService<AmazonDynamoDBClient>(MESSAGES_KEY);
            return new DynamoDbMessageStore(configuration, client);
        });

        return services;
    }
}
