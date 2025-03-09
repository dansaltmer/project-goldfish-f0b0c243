using System.Security.Claims;

namespace Goldfish.RestApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetId(this ClaimsPrincipal user)
    {
        return user.GetClaim("username");
    }

    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.GetClaim("email");
    }

    public static string GetName(this ClaimsPrincipal user)
    {
        return user.GetClaimOrDefault("name") ?? "Unknown";
    }

    public static string? GetAvatar(this ClaimsPrincipal user)
    {
        return user.GetClaimOrDefault("picture");
    }

    private static string GetClaim(this ClaimsPrincipal user, string claim)
    {
        return user.Claims.First(x => x.Type == claim).Value;
    }

    private static string? GetClaimOrDefault(this ClaimsPrincipal user, string claim)
    {
        return user.Claims.FirstOrDefault(x => x.Type == claim)?.Value ?? null;
    }
}
