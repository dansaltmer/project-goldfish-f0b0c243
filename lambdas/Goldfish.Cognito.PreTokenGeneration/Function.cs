using Amazon.Lambda.CognitoEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Goldfish.Cognito.PreTokenGeneration;

public class Function
{
    public CognitoPreTokenGenerationV2Event Handler(CognitoPreTokenGenerationV2Event ev)
    {
        var attributes = ev.Request.UserAttributes;

        var claims = new Dictionary<string, object>
        {
            ["name"] = attributes.GetValueOrDefault("name") ?? "Unknown",
            ["email"] = attributes.GetValueOrDefault("email")!
        };

        if (attributes.TryGetValue("picture", out string? picture))
        {
            claims.Add("picture", picture!);
        }

        ev.Response = new CognitoPreTokenGenerationV2Response
        {
            ClaimsAndScopeOverrideDetails = new ClaimsAndScopeOverrideDetails
            {
                IdTokenGeneration = new IdTokenGeneration
                {
                    ClaimsToAddOrOverride = claims,
                },
                AccessTokenGeneration = new AccessTokenGeneration
                {
                    ClaimsToAddOrOverride = claims,
                },
            },
        };

        return ev;
    }
}
