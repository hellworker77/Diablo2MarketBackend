using Common.Models;

namespace Common.Services.Interfaces;

public interface IItemAttributeService
{
    Task<ItemAttributeDto> GetByIdAsync(Guid itemAttributeId,
        CancellationToken cancellationToken);
    Task<IList<ItemAttributeDto>> GetFromItemAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task AddToItemAsync(Guid itemId,
        Guid userId,
        ItemAttributeDto itemAttributeDto,
        CancellationToken cancellationToken);
    Task EditAsync(Guid itemId, ItemAttributeDto itemAttributeDto,
        Guid userId,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemId,
        Guid itemAttributeId,
        Guid userId,
        CancellationToken cancellationToken);
}