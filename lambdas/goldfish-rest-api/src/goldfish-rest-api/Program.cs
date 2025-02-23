using Amazon;
using Amazon.DynamoDBv2;
using Goldfish.RestApi.Endpoints;
using Goldfish.RestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.AddSingleton(sp => new AmazonDynamoDBClient(new AmazonDynamoDBConfig
{
    RegionEndpoint = RegionEndpoint.EUNorth1
}));

builder.Services.AddTransient<IMessageStore>(sp =>
{
    var tableName = sp.GetRequiredService<IConfiguration>()["Tables:Messages"]!;
    var client = sp.GetRequiredService<AmazonDynamoDBClient>();
    return new DynamoDbMessageStore(tableName, client);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");
app.MapGroup("/{channel}/messages").MapMessagesApi();

app.Run();
