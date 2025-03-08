using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Globalization;
using System.Text.Json;

namespace Goldfish.RestApi.Models.Database;

// Lazy models
public class FeedMessageEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FeedId { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public UserProfile Profile { get; set; } = new UserProfile();

    public FeedMediaItem? Media { get; set; }

    public string Text { get; set; } = string.Empty;

    public Dictionary<string, AttributeValue> ToDynamoItem()
    {
        var item = new Dictionary<string, AttributeValue>
        {
            ["message_id"] = new AttributeValue(Id),
            ["feed_id"] = new AttributeValue(FeedId),
            ["timestamp"] = new AttributeValue(Timestamp.ToString("o", CultureInfo.InvariantCulture)),
            ["profile"] = new AttributeValue(JsonSerializer.Serialize(Profile)),
            ["text"] = new AttributeValue(Text),
        };

        if (Media != null)
        {
            item["media"] = new AttributeValue(JsonSerializer.Serialize(Media));
        }

        return item;
    }

    public static FeedMessageEntity FromDocument(Document doc)
    {
        var message = new FeedMessageEntity
        {
            Id = doc["message_id"],
            FeedId = doc["feed_id"],
            Timestamp = DateTime.Parse(doc["timestamp"]),
            Profile = JsonSerializer.Deserialize<UserProfile>(doc["profile"])!,
            Text = doc["text"],
        };

        if (doc.ContainsKey("media"))
        {
            message.Media = JsonSerializer.Deserialize<FeedMediaItem>(doc["media"]);
        }

        return message;
    }
}
