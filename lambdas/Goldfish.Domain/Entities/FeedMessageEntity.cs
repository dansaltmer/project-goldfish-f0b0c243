namespace Goldfish.Domain.Entities;

// Lazy models
public class FeedMessageEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FeedId { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public UserProfile Profile { get; set; } = new UserProfile();

    public FeedMediaItem? Media { get; set; }

    public string Text { get; set; } = string.Empty;
}
