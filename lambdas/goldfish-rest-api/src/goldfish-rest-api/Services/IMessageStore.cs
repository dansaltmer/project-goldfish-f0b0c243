using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Goldfish.RestApi.Models;

namespace Goldfish.RestApi.Services;

public interface IMessageStore
{
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

    public async Task PutMessageAsync(MessageDto message)
    {
        await _client.PutItemAsync(new PutItemRequest
        {
            TableName = _tableName,
            Item = message.ToDynamoItem()
        });
    }
}
