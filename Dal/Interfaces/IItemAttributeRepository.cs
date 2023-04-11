using Entities;

namespace Dal.Interfaces;

public interface IItemAttributeRepository
{
    Task<ItemAttribute?> GetByIdAsync(Guid itemAttributeId,
        CancellationToken cancellationToken);
    Task<IList<ItemAttribute>> GetFromItemAsync(Guid itemId,
        CancellationToken cancellationToken);
    Task AddAsync(ItemAttribute itemAttribute,
        CancellationToken cancellationToken);
    Task EditAsync(ItemAttribute itemAttribute,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid itemAttributeId,
        CancellationToken cancellationToken);
}