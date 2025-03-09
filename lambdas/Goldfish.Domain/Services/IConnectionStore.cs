
namespace Goldfish.Domain.Services;

public interface IConnectionStore
{
    Task AddAsync(string connectionId, string userId, List<string> feeds);
    Task<IList<string>> GetAllConnectionIdsAsync();
    Task RemoveAsync(string connectionId);
}
