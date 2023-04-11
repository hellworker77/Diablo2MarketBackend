using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class DealMemberRepository : IDealMemberRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<DealMember> _dbSet;

    public DealMemberRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = applicationContext.Set<DealMember>();
    }

    public async Task<DealMember?> GetByIdAsync(Guid dealMemberId,
        CancellationToken cancellationToken)
    {
        var dealMember = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == dealMemberId, cancellationToken);

        return dealMember;
    }
    public async Task<IList<DealMember>> GetByDealIdAsync(Guid dealId,
        CancellationToken cancellationToken)
    {
        var dealMembers = await _dbSet.AsNoTracking()
            .Where(x => x.DealId == dealId)
            .ToListAsync(cancellationToken);

        return dealMembers;
    }

    public async Task CreateAsync(DealMember dealMember,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(dealMember).State = EntityState.Added;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public Task EditAsync(DealMember dealMember,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    public async Task ApproveAsync(DealMember dealMember,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(dealMember).Property(x => x.Approved).IsModified = true;

        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
    public Task DeleteAsync(Guid dealMemberId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}