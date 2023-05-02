using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Common.Services.Interfaces;
using Dal.Data;
using Dal.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly MediaMapper _mapper;

        public MediaService(IMediaRepository mediaRepository,
            MediaMapper mapper)
        {
            _mediaRepository = mediaRepository;
            _mapper = mapper;
        }

        public async Task<MediaDto> GetByIdAsync(Guid mediaId, 
            CancellationToken cancellationToken)
        {
            var media = await _mediaRepository.GetByIdAsync(mediaId, cancellationToken);
            if (media is null)
            {
                throw new EntityNotFoundException(typeof(Media), mediaId);
            }

            return _mapper.Map(media);
        }

        public async Task CreateToItemAsync(MediaDto mediaDto, 
            Guid itemId,
            CancellationToken cancellationToken)
        {
            var media = _mapper.ReverseMap(mediaDto);
            media.ItemId = itemId;

            await _mediaRepository.CreateAsync(media, cancellationToken);
        }

        public async Task CreateToUserAsync(MediaDto mediaDto, 
            Guid userId, 
            CancellationToken cancellationToken)
        {
            var media = _mapper.ReverseMap(mediaDto);
            media.ProfileId = userId;

            await _mediaRepository.CreateAsync(media, cancellationToken);
        }

        public async Task UpdateAsync(MediaDto mediaDto,
            Guid mediaId,
            CancellationToken cancellationToken)
        {
            var media = _mapper.ReverseMap(mediaDto);
            media.Id = mediaId;

            await _mediaRepository.UpdateAsync(media, cancellationToken);
        }

        public async Task DeleteAsync(Guid mediaId, 
            CancellationToken cancellationToken)
        {
            var media = await _mediaRepository.GetByIdAsync(mediaId, cancellationToken);
            if (media is null)
            {
                throw new EntityNotFoundException(typeof(Media), mediaId);
            }

            await _mediaRepository.DeleteAsync(media, cancellationToken);
        }
    }
}
