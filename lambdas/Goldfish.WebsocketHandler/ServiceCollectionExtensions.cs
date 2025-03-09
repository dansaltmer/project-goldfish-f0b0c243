using Goldfish.DynamoDb.Connections;
using Goldfish.DynamoDb.Connections.Configuration;
using Goldfish.WebsocketHandler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goldfish.WebsocketHandler;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebsocketHandlerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionsConfiguration = configuration.GetSection("Connections").Get<DynamoDbConnectionsConfiguration>()!;
        services.AddDynamoDbConnectionsStore(connectionsConfiguration);

        services.AddSingleton<IRequestValidator, SketchyRequestValidator>();

        return services;
    }
}
