using Goldfish.RestApi.Models.Database;

namespace Goldfish.RestApi.Services;

public interface IMessageStore
{
    Task<IList<FeedMessageEntity>> GetMessagesAsync(string feed);
    Task PutMessageAsync(FeedMessageEntity message);
}
