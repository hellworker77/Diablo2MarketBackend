using Common.Models;

namespace Common.Services.Interfaces;

public interface IItemService
{
    Task<ItemDto> GetByIdAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task<IList<ItemDto>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task AddAsync(ItemDto itemDto,
        CancellationToken cancellationToken);

    Task EditAsync(ItemDto itemDto,
        CancellationToken cancellationToken);
    Task RaiseAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemId,
        CancellationToken cancellationToken);
}