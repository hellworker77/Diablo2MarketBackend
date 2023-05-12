using Dal.Data;
using Dal.Interfaces;
using Entities;
using Filters.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class DealRepository : IDealRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<Deal> _dbSet;

    public DealRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = applicationContext.Set<Deal>();
    }

    public async Task<Deal?> GetByIdAsync(Guid dealId,
        CancellationToken cancellationToken)
    {
        var deal = await _dbSet.AsNoTracking()
            .Include(x => x.Discussion!)
            .ThenInclude(x => x.Members)
            .Include(x => x.DealMembers)
            .Include(x => x.Goods)
            .ThenInclude(x => x.Attributes)
            .Include(x => x.Goods)
            .ThenInclude(x => x.Media)
            .FirstOrDefaultAsync(x => x.Id == dealId, cancellationToken);

        return deal;
    }

    public async Task<IList<Deal>> GetFilteredChunkAsync(int index, 
        int size, 
        AbstractFilterSpecification<Deal> abstractFilterSpecification, 
        CancellationToken cancellationToken)
    {
        var deals = await _dbSet.AsNoTracking()
           .Include(x => x.Discussion!)
           .ThenInclude(x => x.Members)
           .Include(x => x.DealMembers)
           .Include(x => x.Goods)
           .ThenInclude(x => x.Attributes)
           .Include(x => x.Goods)
           .ThenInclude(x => x.Media)
           .Where(abstractFilterSpecification.SpecificationExpression)
           .Skip(index * size)
           .Take(size)
           .ToListAsync(cancellationToken);

        return deals;
    }

    public async Task<IList<Deal>> GetChunkAsync(Guid userId,
        int index,
        int size,
        CancellationToken cancellationToken)
    {
        var deals = await _dbSet.AsNoTracking()
            .Include(x => x.Discussion!)
            .ThenInclude(x => x.Members)
            .Include(x => x.DealMembers)
            .Include(x => x.Goods)
            .ThenInclude(x=> x.Attributes)
            .Include(x => x.Goods)
            .ThenInclude(x => x.Media)
            .Where(x => x.DealMembers.Any(c => c.UserId == userId))
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return deals;
    }

    public async Task<int> GetDealsCountAsync(CancellationToken cancellationToken)
    {
        var dealsCount = await _dbSet.CountAsync(cancellationToken);

        return dealsCount;
    }

    public async Task<IList<Deal>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken)
    {
        var deals = await _dbSet.AsNoTracking()
            .Include(x => x.Discussion!)
            .ThenInclude(x=>x.Members)
            .Include(x => x.DealMembers)
            .ThenInclude(x=>x.User)
            .Include(x => x.Goods)
            .ThenInclude(x=>x.Attributes)
            .Include(x => x.Goods)
            .ThenInclude(x => x.Media)
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return deals;
    }

    public async Task<int> GetUserDealsCountAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var dealsCount = await _dbSet.AsNoTracking()
            .Include(x => x.DealMembers)
            .Where(x => x.DealMembers.Any(c => c.UserId == userId))
            .CountAsync(cancellationToken);

        return dealsCount;
    }

    public async Task CreateAsync(Deal deal,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(deal).State = EntityState.Added;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(Deal deal,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(deal).State = EntityState.Modified;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task EditStatusAsync(Deal deal,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(deal).Property(x => x.Status).IsModified = true;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Deal deal,
        CancellationToken cancellationToken)
    {
        _dbSet.Remove(deal);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}