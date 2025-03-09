using Goldfish.Domain.Entities;

namespace Goldfish.Domain.Services;

public interface IMessageStore
{
    Task<IList<FeedMessageEntity>> GetMessagesAsync(string feed);
    Task PutMessageAsync(FeedMessageEntity message);
}
