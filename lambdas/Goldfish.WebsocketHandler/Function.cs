using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Goldfish.Domain.Services;
using Goldfish.WebsocketHandler.Extensions;
using Goldfish.WebsocketHandler.Helpers;
using Goldfish.WebsocketHandler.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Goldfish.WebsocketHandler;

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
            .AddWebsocketHandlerServices(_configuration)
            .BuildServiceProvider();

        _scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        using var scope = _scopeFactory.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IRequestValidator>();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            return ProxyResponse.Unauthorized();
        }

        context.Logger.LogInformation(JsonSerializer.Serialize(request));

        // improve to factory pattern if this gets unwieldy
        var store = scope.ServiceProvider.GetRequiredService<IConnectionStore>();
        switch (request.RequestContext.RouteKey)
        {
            case "$connect":
                await store.AddAsync(request.RequestContext.ConnectionId, result.Result!.GetId(), ["welcome"]);
                break;
            case "$disconnect":
                await store.RemoveAsync(request.RequestContext.ConnectionId);
                break;
        }

        return ProxyResponse.Ok();
    }
}
