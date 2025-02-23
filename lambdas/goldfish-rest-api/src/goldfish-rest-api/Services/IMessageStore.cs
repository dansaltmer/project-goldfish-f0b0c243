using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Goldfish.RestApi.Models;

namespace Goldfish.RestApi.Services;

public interface IMessageStore
{
    Task<IList<MessageDto>> GetMessagesAsync(string channel);
    Task PutMessageAsync(MessageDto message);
}

public class DynamoDbMessageStore : IMessageStore
{
    private readonly AmazonDynamoDBClient _client;
    private readonly string _tableName;

    public DynamoDbMessageStore(string tableName, AmazonDynamoDBClient client)
    {
        _client = client;
        _tableName = tableName;
    }

    public async Task<IList<MessageDto>> GetMessagesAsync(string channel)
    {
        var table = Table.LoadTable(_client, _tableName);        
        var filter = new QueryFilter("channel_id", QueryOperator.Equal, channel);
        var config = new QueryOperationConfig
        {
            IndexName = "channel_id-timestamp-index",
            Filter = filter,
            Limit = 10,
            BackwardSearch = true,
        };

        var search = table.Query(config);

        var batch = await search.GetNextSetAsync();

        return batch.Select(MessageDto.FromDocument).ToList();
    }

    public async Task PutMessageAsync(MessageDto message)
    {
        await _client.PutItemAsync(new PutItemRequest
        {
            TableName = _tableName,
            Item = message.ToDynamoItem()
        });
    }
}
