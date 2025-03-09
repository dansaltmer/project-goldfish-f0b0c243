using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Goldfish.WebsocketHandler.Services;

public class SketchyRequestValidator : IRequestValidator
{
    private const string AUTH_HEADER = "Authorization";
    private const string SCOPE_KEY = "Authentication:Scope";
    private const string AUTHORITY_KEY = "Authentication:Authority";

    private string _scope;
    private string _authority;
    private TokenValidationParameters _tokenValidatorParameters;

    public SketchyRequestValidator(IConfiguration configuration)
    {
        _scope = configuration[SCOPE_KEY] ?? throw new ArgumentException(SCOPE_KEY);
        _authority = configuration[AUTHORITY_KEY] ?? throw new ArgumentException(AUTHORITY_KEY);

        // being lazy, asbtract
        var keysResponse = new HttpClient().Send(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_authority}/.well-known/jwks.json")
        });

        using var reader = new StreamReader(keysResponse.Content.ReadAsStream());
        var jsonKeys = reader.ReadToEnd();
        var keys = JsonWebKeySet.Create(jsonKeys);

        _tokenValidatorParameters = new TokenValidationParameters
        {
            ValidIssuer = _authority,
            IssuerSigningKeys = keys.GetSigningKeys(),
            AudienceValidator = (_, token, _) =>
            {
                if (token is not JwtSecurityToken jwt) return false;

                // AWS access tokens don't seem to provide an audience, shouldn't really validate the client_id it provides
                // as it prevents granting access to multiple client apps, the api should be agnostic of the client.
                // So validating the scope here instead, needs rethinking if you need multiple scopes on the resource
                // Need to do some reading on recommended approach without audience, or could build into the lambda token generation
                var scopes = jwt.Claims.FirstOrDefault(x => x.Type == "scope")?.Value.Split(' ') ?? [];

                return scopes.Contains(_scope);
            }
        };
    }

    public async Task<RequestValidatorResult> ValidateAsync(APIGatewayProxyRequest request)
    {
        if (request.Headers == null 
            || !request.Headers.TryGetValue(AUTH_HEADER, out string? value) 
            || !value.StartsWith("Bearer "))
        {
            return RequestValidatorResult.NotValid();
        }

        var token = value.Substring(7);

        var result = await new JwtSecurityTokenHandler()
            .ValidateTokenAsync(token, _tokenValidatorParameters);

        if (result == null || !result.IsValid)
        {
            return RequestValidatorResult.NotValid();
        }

        return RequestValidatorResult.Valid(result.ClaimsIdentity);
    }
}
