using Amazon;
using Amazon.ApiGatewayManagementApi;
using Amazon.ApiGatewayManagementApi.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Goldfish.Domain.Services;
using Goldfish.DynamoDb.Messages.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Goldfish.NotifyConsumer;

public class Function
{
    private static readonly IConfiguration _configuration;
    private static readonly IServiceProvider _serviceProvider;
    private static readonly IServiceScopeFactory _scopeFactory;

    static Function()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
            .AddJsonFile("appsettings.json", false)
            .AddEnvironmentVariables()
            .Build();

        _serviceProvider = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddNotifyConsumerServices(_configuration)
            .BuildServiceProvider();

        _scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task Handler(DynamoDBEvent ev, ILambdaContext context)
    {
        using var scope = _scopeFactory.CreateScope();

        var items = ev.Records
            .Where(x => x.EventName == "INSERT")
            // ew? `Document.FromAttributeMap` doesn't seem to work on the event attribute map
            .Select(x => Document.FromJson(x.Dynamodb.NewImage.ToJson()).ToFeedMessageEntity()) 
            .ToList();

        var api = new AmazonApiGatewayManagementApiClient(new AmazonApiGatewayManagementApiConfig
        {
            RegionEndpoint = RegionEndpoint.EUNorth1,
            ServiceURL = _configuration["Websockets:ApiEndpoint"]
        });

        var connectionStore = scope.ServiceProvider.GetRequiredService<IConnectionStore>();
        var connections = await connectionStore.GetAllConnectionIdsAsync();

        context.Logger.LogInformation($"Sending to {connections.Count} connections");
        context.Logger.LogInformation(JsonSerializer.Serialize(items));

        var tasks = connections.Select(connectionId =>
        {
            return Task.Run(async () =>
            {
                try
                {
                    foreach (var item in items)
                    {
                        await api.PostToConnectionAsync(new PostToConnectionRequest
                        {
                            ConnectionId = connectionId,
                            Data = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(item))
                        });
                    }
                }
                catch (GoneException)
                {
                    context.Logger.LogDebug($"Removing Gone: {connectionId}");
                    await connectionStore.RemoveAsync(connectionId);
                }
            });
        });

        await Task.WhenAll(tasks);
    }
}
