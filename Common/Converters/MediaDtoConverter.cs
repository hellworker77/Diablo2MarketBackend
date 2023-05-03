using Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Converters
{
    public static class MediaDtoConverter
    {
        public static MediaDto ToMedia(this IFormFile formFile)
        {
            var fileReadStream = formFile.OpenReadStream();
            byte[] buffer = new byte[fileReadStream.Length];

            fileReadStream.Read(buffer, 0, buffer.Length);

            var mediaDto = new MediaDto
            {
                Type = Entities.Enums.MediaType.Photo,
                Data = buffer
            };

            return mediaDto;
        }
    }
}
