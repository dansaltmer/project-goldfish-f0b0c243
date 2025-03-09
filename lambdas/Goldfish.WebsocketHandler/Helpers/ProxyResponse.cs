using Amazon.Lambda.APIGatewayEvents;
using System.Net;

namespace Goldfish.WebsocketHandler.Helpers;

public static class ProxyResponse
{
    public static APIGatewayProxyResponse Ok(string body = "OK")
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = body
        };
    }

    public static APIGatewayProxyResponse Unauthorized()
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.Unauthorized,
            Body = "Unauthorized"
        };
    }
}
