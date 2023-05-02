using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Interfaces
{
    public interface IMediaService
    {
        Task<MediaDto> GetByIdAsync(Guid mediaId,
            CancellationToken cancellationToken);
        Task CreateToItemAsync(MediaDto mediaDto,
            Guid itemId,
            CancellationToken cancellationToken);
        Task CreateToUserAsync(MediaDto mediaDto,
            Guid userId,
            CancellationToken cancellationToken);
        Task UpdateAsync(MediaDto mediaDto,
            Guid mediaId,
            CancellationToken cancellationToken);
        Task DeleteAsync(Guid mediaId,
            CancellationToken cancellationToken);
    }
}
