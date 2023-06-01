using Common.Models;

namespace Common.Services.Interfaces;

public interface IItemService
{
    Task<ItemDto> GetByIdAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task<IList<ItemDto>> GetChunkOrderByPostedDateAsync(int index,
            int size,
            CancellationToken cancellationToken);
    Task<IList<ItemDto>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task<int> GetItemsCountAsync(CancellationToken cancellationToken);
    Task<IList<ItemDto>> GetUserChunkAsync(Guid userId,
            int index,
            int size,
            CancellationToken cancellationToken);
    Task<int> GetUserItemsCountAsync(Guid userId,
        CancellationToken cancellationToken);
    Task<Guid> AddAsync(ItemDto itemDto,
        CancellationToken cancellationToken);
    Task EditAsync(ItemDto itemDto,
        Guid userId,
        CancellationToken cancellationToken);
    Task RaiseAsync(Guid itemId,
        Guid userId,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemId,
        Guid userId,
        CancellationToken cancellationToken);
}