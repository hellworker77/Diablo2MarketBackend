using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class ItemAttributeRepository : IItemAttributeRepository
{
    private readonly ApplicationContext _applicationContext;
    private readonly DbSet<ItemAttribute> _dbSet;

    public ItemAttributeRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        _dbSet = applicationContext.Set<ItemAttribute>();
    }

    public async Task<ItemAttribute?> GetByIdAsync(Guid itemAttributeId,
        CancellationToken cancellationToken)
    {
        var itemAttribute = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == itemAttributeId, cancellationToken);

        return itemAttribute;
    }

    public async Task<IList<ItemAttribute>> GetFromItemAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        var itemAttributes = await _dbSet.AsNoTracking()
            .Where(x=>x.ItemId == itemId)
            .ToListAsync(cancellationToken);

        return itemAttributes;
    }

    public async Task AddAsync(ItemAttribute itemAttribute,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(itemAttribute).State = EntityState.Added;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }

    public async Task EditAsync(ItemAttribute itemAttribute,
        CancellationToken cancellationToken)
    {
        _applicationContext.Entry(itemAttribute).State = EntityState.Modified;
        _applicationContext.Entry(itemAttribute).Property(x => x.ItemId).IsModified = false;
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteAsync(Guid itemAttributeId,
        CancellationToken cancellationToken)
    {
        var itemAttribute = await _dbSet.FindAsync(itemAttributeId, cancellationToken);

        _dbSet.Remove(itemAttribute!);
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}