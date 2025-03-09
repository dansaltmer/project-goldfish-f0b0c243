using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Goldfish.Domain.Services;
using Goldfish.DynamoDb.Connections.Configuration;

namespace Goldfish.DynamoDb.Connections.Services;

public class DynamoDbConnectionStore : IConnectionStore
{
    private readonly DynamoDbConnectionsConfiguration _configuration;
    private readonly AmazonDynamoDBClient _client;

    public DynamoDbConnectionStore(DynamoDbConnectionsConfiguration configuration, AmazonDynamoDBClient client)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task<IList<string>> GetAllConnectionIdsAsync()
    {
        var results = await _client.ScanAsync(new ScanRequest
        {
            TableName = _configuration.TableName,
            AttributesToGet = ["connection_id"]
        });

        return results.Items.Select(x => x["connection_id"].S).ToList();
    }

    public async Task AddAsync(string connectionId, string userId, List<string> feeds)
    {
        var expires = $"{DateTimeOffset.UtcNow.Add(_configuration.TTL).ToUnixTimeSeconds()}";

        await _client.PutItemAsync(new PutItemRequest
        {
            TableName = _configuration.TableName,
            Item = new Dictionary<string, AttributeValue>
            {
                ["connection_id"] = new AttributeValue(connectionId),
                ["user_id"] = new AttributeValue(userId),
                ["feeds"] = new AttributeValue(feeds),
                ["expires"] = new AttributeValue { N = expires }
            }
        });
    }

    public async Task RemoveAsync(string connectionId)
    {
        await _client.DeleteItemAsync(new DeleteItemRequest
        {
            TableName = _configuration.TableName,
            Key = new Dictionary<string, AttributeValue>
            {
                ["connection_id"] = new AttributeValue(connectionId)
            }
        });
    }
}
