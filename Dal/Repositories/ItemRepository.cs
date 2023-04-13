using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<Item> _dbSet;

    public ItemRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = applicationContext.Set<Item>();
    }
    public async Task<Item?> GetByIdAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        var item = await _dbSet.AsNoTracking()
            .Include(x => x.Deal)
            .Include(x => x.Owner)
            .Include(x => x.Attributes)
            .FirstOrDefaultAsync(x => x.Id == itemId, cancellationToken);

        return item;
    }

    public async Task<IList<Item>> GetChunkOrderByPostedDateAsync(int index,
        int size,
        CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Deal)
            .Include(x => x.Owner)
            .Include(x => x.Attributes)
            .OrderBy(x => x.PostedDate)
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Item>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken)
    {
        var items = await _dbSet.AsNoTracking()
            .Include(x => x.Deal)
            .Include(x => x.Owner)
            .Include(x => x.Attributes)
            .Skip(index * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return items;
    }
    public async Task AddAsync(Item item,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(item).State = EntityState.Added;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
    public async Task EditAsync(Item item,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(item).State = EntityState.Modified;
        _applicationContext.Entry(item).Collection(x => x.Attributes!).IsModified = false;
        _applicationContext.Entry(item).Property(x => x.OwnerId).IsModified = false;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        var item = await _dbSet.FindAsync(itemId, cancellationToken);

        _dbSet.Remove(item!);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}