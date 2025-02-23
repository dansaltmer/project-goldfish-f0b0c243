using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Globalization;
using System.Text.Json;

namespace Goldfish.RestApi.Models;

// Lazy models
public class MessageDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ChannelId { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public ProfileDto Profile { get; set; } = new ProfileDto();

    public MediaDto? Media { get; set; }

    public string Text { get; set; } = string.Empty;

    public Dictionary<string, AttributeValue> ToDynamoItem()
    {
        var item = new Dictionary<string, AttributeValue>
        {
            ["message_id"] = new AttributeValue(Id),
            ["channel_id"] = new AttributeValue(ChannelId),
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

    public static MessageDto FromDocument(Document doc)
    {
        var message = new MessageDto
        {
            Id = doc["message_id"],
            ChannelId = doc["channel_id"],
            Timestamp = DateTime.Parse(doc["timestamp"]),
            Profile = JsonSerializer.Deserialize<ProfileDto>(doc["profile"])!,
            Text = doc["text"],
        };

        if (doc.ContainsKey("media"))
        {
            message.Media = JsonSerializer.Deserialize<MediaDto>(doc["media"]);
        }

        return message;
    }
}

public class ProfileDto
{
    public string Name { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
}

public class MediaDto
{
    public string Type { get; set; } = "";

    public string? Url { get; set; }
}
