namespace Goldfish.RestApi.Models.Database;

public class FeedMediaItem
{
    public string Type { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public static FeedMediaItem Iframe(string url)
    {
        return new FeedMediaItem { Type = Constants.MediaTypes.IFRAME, Url = url };
    }

    public static FeedMediaItem Image(string url)
    {
        return new FeedMediaItem { Type = Constants.MediaTypes.IMAGE, Url = url };
    }
}
