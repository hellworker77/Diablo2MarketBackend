using Entities;

namespace Dal.Interfaces;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task<IList<Item>> GetChunkOrderByPostedDateAsync(int index,
            int size,
            CancellationToken cancellationToken);
    Task<IList<Item>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task<IList<Item>> GetUserChunkAsync(Guid userId,
        int index,
        int size,
        CancellationToken cancellationToken);
    Task AddAsync(Item item,
        CancellationToken cancellationToken);
    Task EditAsync(Item item,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemId,
        CancellationToken cancellationToken);
}