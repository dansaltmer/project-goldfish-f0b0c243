using Amazon;
using Amazon.DynamoDBv2;
using Goldfish.RestApi.Endpoints;
using Goldfish.RestApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = builder.Configuration["Authentication:Authority"];

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            AudienceValidator = (_, token, _) =>
            {
                if (token is not JsonWebToken jwt) return false;

                // AWS access tokens don't seem to provide an audience, shouldn't really validate the client_id it provides
                // as it prevents granting access to multiple client apps, the api should be agnostic of the client.
                // So validating the scope instead here instead, needs rethinking if you need multiple scopes on the resource
                // Need to do some reading on recommended approach without audience, or could build into the lambda token generation
                var scopes = jwt.GetPayloadValue<string>("scope")?.Split(' ') ?? [];

                return scopes.Contains(builder.Configuration["Authentication:Scope"]);
            }
        };
    });

builder.Services.AddSingleton(sp => new AmazonDynamoDBClient(new AmazonDynamoDBConfig
{
    RegionEndpoint = RegionEndpoint.EUNorth1
}));

builder.Services.AddTransient<IMessageStore>(sp =>
{
    var tableName = builder.Configuration["Tables:Messages"]!;
    var client = sp.GetRequiredService<AmazonDynamoDBClient>();
    return new DynamoDbMessageStore(tableName, client);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.MapGroup("/{feed}/messages").MapMessagesApi().RequireAuthorization();

app.Run();
