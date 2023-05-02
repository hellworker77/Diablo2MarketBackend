using Common.Converters;
using Common.Models;
using Common.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Media.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMediaService _mediaService;

        public MediaController(IIdentityService identityService,
            IMediaService mediaService)
        {
            _identityService = identityService;
            _mediaService = mediaService;
        }

        [HttpGet("id")]
        public async Task<MediaDto> GetByIdAsync(Guid mediaId,
            CancellationToken cancellationToken)
        {
            var mediaDto = await _mediaService.GetByIdAsync(mediaId, cancellationToken);

            return mediaDto;
        }

        [Authorize]
        [HttpPost("createToItem")]
        public async Task<IActionResult> CreateToItemAsync(IFormFile formFile,
            Guid itemId,
            CancellationToken cancellationToken)
        {
            var mediaDto = formFile.ToMedia();

            await _mediaService.CreateToItemAsync(mediaDto, itemId, cancellationToken);

            return Ok("File created!");
        }

        [Authorize]
        [HttpPost("createToUser")]
        public async Task<IActionResult> CreateToUserAsync(IFormFile formFile,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var mediaDto = formFile.ToMedia();

            await _mediaService.CreateToUserAsync(mediaDto, userId, cancellationToken);

            return Ok("File created!");
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(IFormFile formFile,
            Guid mediaId,
            CancellationToken cancellationToken)
        {
            var mediaDto = formFile.ToMedia();

            await _mediaService.UpdateAsync(mediaDto, mediaId, cancellationToken);

            return Ok("File updated!");
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Guid mediaId,
            CancellationToken cancellationToken)
        {
            await _mediaService.DeleteAsync(mediaId, cancellationToken);

            return Ok("File deleted!");
        }
    }
}
