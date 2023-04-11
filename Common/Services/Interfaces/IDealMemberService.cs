using Common.Models;
using Entities.Enums;

namespace Common.Services.Interfaces;

public interface IDealMemberService
{
    Task CreateAsync(Guid dealId,
        Guid userId,
        DealMemberStatus status,
        CancellationToken cancellationToken);
    Task<IList<DealMemberDto>> GetByDealIdAsync(Guid dealId,
        CancellationToken cancellationToken);
    Task ApproveAsync(Guid dealMemberId,
        CancellationToken cancellationToken);
}