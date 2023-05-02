using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly DbSet<Media> _dbSet;

        public MediaRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = _applicationContext.Set<Media>();
        }

        public async Task<Media> GetByIdAsync(Guid mediaId, 
            CancellationToken cancellationToken)
        {
            var media = await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == mediaId, cancellationToken);

            return media!;
        }

        public async Task CreateAsync(Media media,
            CancellationToken cancellationToken)
        {
            _applicationContext.Entry(media).State = EntityState.Added;
            await _applicationContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Media media, 
            CancellationToken cancellationToken)
        {
            _applicationContext.Entry(media).State = EntityState.Modified;
            await _applicationContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Media media, 
            CancellationToken cancellationToken)
        {
            _dbSet.Remove(media);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
