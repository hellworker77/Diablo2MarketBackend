using Entities;

namespace Dal.Interfaces;

public interface IDealMemberRepository
{
    Task<DealMember?> GetByIdAsync(Guid dealMemberId, 
        CancellationToken cancellationToken);
    Task<IList<DealMember>> GetByDealIdAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task CreateAsync(DealMember dealMember, 
        CancellationToken cancellationToken);
    Task EditAsync(DealMember dealMember, 
        CancellationToken cancellationToken);
    Task ApproveAsync(DealMember dealMember,
        CancellationToken cancellationToken);
    Task DeleteAsync(Guid dealMemberId, 
        CancellationToken cancellationToken);
}