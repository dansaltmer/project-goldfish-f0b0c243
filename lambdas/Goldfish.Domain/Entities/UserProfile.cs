namespace Goldfish.Domain.Entities;

public class UserProfile
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Avatar { get; set; }
}
