using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Goldfish.Domain.Entities;
using System.Globalization;
using System.Text.Json;

namespace Goldfish.DynamoDb.Messages.Mappers;

public static class FeedMessageEntityMapper
{
    public static Dictionary<string, AttributeValue> ToDynamoItem(this FeedMessageEntity entity)
    {
        var item = new Dictionary<string, AttributeValue>
        {
            ["message_id"] = new AttributeValue(entity.Id),
            ["feed_id"] = new AttributeValue(entity.FeedId),
            ["timestamp"] = new AttributeValue(entity.Timestamp.ToString("o", CultureInfo.InvariantCulture)),
            ["profile"] = new AttributeValue(JsonSerializer.Serialize(entity.Profile)),
            ["text"] = new AttributeValue(entity.Text),
        };

        if (entity.Media != null)
        {
            item["media"] = new AttributeValue(JsonSerializer.Serialize(entity.Media));
        }

        return item;
    }

    public static FeedMessageEntity ToFeedMessageEntity(this Document doc)
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
