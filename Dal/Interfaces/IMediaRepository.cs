using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IMediaRepository
    {
        Task<Media> GetByIdAsync(Guid mediaId, 
            CancellationToken cancellationToken);
        Task CreateAsync(Media media,
            CancellationToken cancellationToken);
        Task UpdateAsync(Media media, 
            CancellationToken cancellationToken);
        Task DeleteAsync(Media media, 
            CancellationToken cancellationToken);
    }
}
