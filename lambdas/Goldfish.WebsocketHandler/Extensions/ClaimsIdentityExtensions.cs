using System.Security.Claims;

namespace Goldfish.WebsocketHandler.Extensions;

public static class ClaimsIdentityExtensions
{
    public static string GetId(this ClaimsIdentity user)
    {
        return user.Claims.First(x => x.Type == "username").Value;
    }
}
