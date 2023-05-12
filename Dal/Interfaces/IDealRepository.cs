using Entities;
using Filters.Abstractions;

namespace Dal.Interfaces;

public interface IDealRepository
{
    Task<Deal?> GetByIdAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task<IList<Deal>> GetFilteredChunkAsync(int index,
        int size,
        AbstractFilterSpecification<Deal> abstractFilterSpecification,
        CancellationToken cancellationToken);
    Task<IList<Deal>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task<int> GetDealsCountAsync(CancellationToken cancellationToken);
    Task<IList<Deal>> GetChunkAsync(Guid userId,
        int index,
        int size,
        CancellationToken cancellationToken);
    Task<int> GetUserDealsCountAsync(Guid userId,
        CancellationToken cancellationToken);
    Task CreateAsync(Deal deal,
        CancellationToken cancellationToken);
    Task EditAsync(Deal deal,
        CancellationToken cancellationToken);
    Task EditStatusAsync(Deal deal,
        CancellationToken cancellationToken);
    Task DeleteAsync(Deal deal,
        CancellationToken cancellationToken);
}