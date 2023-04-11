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
    Task EditAsync(ItemAttributeDto itemAttributeDto,
        Guid userId,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemAttributeId,
        Guid userId,
        CancellationToken cancellationToken);
}