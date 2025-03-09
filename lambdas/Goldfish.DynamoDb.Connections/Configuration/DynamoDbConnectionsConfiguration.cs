namespace Goldfish.DynamoDb.Connections.Configuration;

public class DynamoDbConnectionsConfiguration
{
    public string TableName { get; set; } = string.Empty;
    public TimeSpan TTL { get; set; }
}
