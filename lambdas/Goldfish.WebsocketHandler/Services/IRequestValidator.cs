using Amazon.Lambda.APIGatewayEvents;
using System.Security.Claims;

namespace Goldfish.WebsocketHandler.Services;

public interface IRequestValidator
{
    Task<RequestValidatorResult> ValidateAsync(APIGatewayProxyRequest request);
}

public class RequestValidatorResult
{
    public bool IsValid { get; set; }

    public ClaimsIdentity? Result { get; set; }

    public static RequestValidatorResult NotValid()
    {
        return new RequestValidatorResult
        {
            IsValid = false,
        };
    }

    public static RequestValidatorResult Valid(ClaimsIdentity result)
    {
        return new RequestValidatorResult
        {
            IsValid = true,
            Result = result
        };
    }
}
