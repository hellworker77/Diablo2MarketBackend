using Common.Models;

namespace Common.Services.Interfaces;

public interface IDealService
{
    Task<DealDto> GetByIdAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task<IList<DealDto>> GetUserChunkAsync(Guid userId,
        int index,
        int size,
        CancellationToken cancellationToken);
    Task<IList<DealDto>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken);
    Task CreateAsync(Guid itemId,
        Guid userId,
        CancellationToken cancellationToken);
    Task ApproveAsync(Guid dealId,
        Guid userId,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task EditAsync(DealDto dealDto,
        CancellationToken cancellationToken);
}