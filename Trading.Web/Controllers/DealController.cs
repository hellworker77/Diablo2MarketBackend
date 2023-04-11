using Common.Services;
using Common.Services.Interfaces;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IDealService _dealService;
        private readonly IIdentityService _identityService;

        public DealController(IDealService dealService,
            IIdentityService identityService)
        {
            _dealService = dealService;
            _identityService = identityService;
        }

        [HttpGet("id")]
        public Task<IActionResult> GetByIdAsync(Guid dealId,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("chunk")]
        public async Task<IActionResult> GetChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var deals = await _dealService.GetChunkAsync(index, size, cancellationToken);

            return Ok(deals);
        }

        [HttpGet("userChunk")]
        public async Task<IActionResult> GetUserChunkAsync(Guid userId,
            int index,
            int size,
            CancellationToken cancellationToken)
        {
            var deals = await _dealService.GetUserChunkAsync(userId, index, size, cancellationToken);
            return Ok(deals);
        }

        [Authorize]
        [HttpGet("ownChunk")]
        public async Task<IActionResult> GetUserChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var deals = await _dealService.GetUserChunkAsync(userId, index, size, cancellationToken);
            return Ok(deals);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _dealService.CreateAsync(itemId, userId, cancellationToken);

            return Ok("Deal created");
        }

        [Authorize]
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveAsync(Guid dealId,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _dealService.ApproveAsync(dealId, userId, cancellationToken);
            return Ok("You approved deal");
        }

        [HttpPut("status")]
        public Task<IActionResult> ChangeStatusAsync(Guid dealId,
            DealStatus dealStatus,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("delete")]
        public Task<IActionResult> DeleteAsync(Guid dealId,
            DealStatus dealStatus,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
