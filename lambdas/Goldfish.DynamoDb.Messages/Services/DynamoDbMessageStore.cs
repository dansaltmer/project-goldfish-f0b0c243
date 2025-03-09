using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Goldfish.Domain.Entities;
using Goldfish.Domain.Services;
using Goldfish.DynamoDb.Messages.Mappers;

namespace Goldfish.DynamoDb.Messages.Services;

public class DynamoDbMessageStore : IMessageStore
{
    private readonly DynamoDbMessagesConfiguration _configuration;
    private readonly AmazonDynamoDBClient _client;

    public DynamoDbMessageStore(DynamoDbMessagesConfiguration configuration, AmazonDynamoDBClient client)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task<IList<FeedMessageEntity>> GetMessagesAsync(string feed)
    {
        var table = Table.LoadTable(_client, _configuration.TableName);
        var filter = new QueryFilter("feed_id", QueryOperator.Equal, feed);
        var config = new QueryOperationConfig
        {
            IndexName = "feed_id-timestamp-index",
            Filter = filter,
            Limit = 10,
            BackwardSearch = true,
        };

        var search = table.Query(config);

        var batch = await search.GetNextSetAsync();

        return batch.Select(x => x.ToFeedMessageEntity()).ToList();
    }

    public async Task PutMessageAsync(FeedMessageEntity message)
    {
        await _client.PutItemAsync(new PutItemRequest
        {
            TableName = _configuration.TableName,
            Item = message.ToDynamoItem()
        });
    }
}
