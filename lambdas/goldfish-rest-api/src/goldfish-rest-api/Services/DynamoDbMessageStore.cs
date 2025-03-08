using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Goldfish.RestApi.Models.Database;

namespace Goldfish.RestApi.Services;

public class DynamoDbMessageStore : IMessageStore
{
    private readonly AmazonDynamoDBClient _client;
    private readonly string _tableName;

    public DynamoDbMessageStore(string tableName, AmazonDynamoDBClient client)
    {
        _client = client;
        _tableName = tableName;
    }

    public async Task<IList<FeedMessageEntity>> GetMessagesAsync(string feed)
    {
        var table = Table.LoadTable(_client, _tableName);        
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

        return batch.Select(FeedMessageEntity.FromDocument).ToList();
    }

    public async Task PutMessageAsync(FeedMessageEntity message)
    {
        await _client.PutItemAsync(new PutItemRequest
        {
            TableName = _tableName,
            Item = message.ToDynamoItem()
        });
    }
}
