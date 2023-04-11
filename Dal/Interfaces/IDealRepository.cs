using Entities;

namespace Dal.Interfaces;

public interface IDealRepository
{
    Task<Deal?> GetByIdAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task<IList<Deal>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task<IList<Deal>> GetChunkAsync(Guid userId,
        int index,
        int size,
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