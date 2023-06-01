using Common.Services;
using Common.Services.Interfaces;
using Entities;
using Entities.Enums;
using Filters;
using Filters.Abstractions;
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

        [HttpGet("last24HoursDeals")]
        public async Task<IActionResult> GetLast24HoursDealsChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var deals = await _dealService.GetFilteredChunkAsync(index, size, DealFilterSpecifications.Last24Hours, cancellationToken);

            return Ok(deals);
        }

        [HttpGet("chunk")]
        public async Task<IActionResult> GetChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var deals = await _dealService.GetChunkAsync(index, size, cancellationToken);

            return Ok(deals);
        }

        [HttpGet("dealsCount")]
        public async Task<IActionResult> GetDealsCountAsync(CancellationToken cancellationToken)
        {
            var dealsCount = await _dealService.GetDealsCountAsync(cancellationToken);

            return Ok(dealsCount);
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

        [HttpGet("userDealsCount")]
        public async Task<IActionResult> GetUserDealsCountAsync(Guid userId,
            CancellationToken cancellationToken)
        {
            var dealsCount = await _dealService.GetUserDealsCountAsync(userId, cancellationToken);

            return Ok(dealsCount);
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
        [HttpGet("ownDealsCount")]
        public async Task<IActionResult> GetUserDealsCountAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var dealsCount = await _dealService.GetUserDealsCountAsync(userId, cancellationToken);

            return Ok(dealsCount);
        }

        [Authorize]
        [HttpGet("inProgressDealsChunk")]
        public async Task<IActionResult> GetInProgressDealsChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var deals = await _dealService.GetFilteredChunkAsync(userId, index, size, DealFilterSpecifications.InProgress, cancellationToken);

            return Ok(deals);
        }

        [Authorize]
        [HttpGet("inProgressDealsCount")]
        public async Task<IActionResult> GetInProgressDealsCountAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var dealsCount = await _dealService.GetFilteredDealsCountAsync(userId, DealFilterSpecifications.InProgress, cancellationToken);

            return Ok(dealsCount);
        }

        [Authorize]
        [HttpGet("suspendedDealsChunk")]
        public async Task<IActionResult> GetSuspendedDealsChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var deals = await _dealService.GetFilteredChunkAsync(userId, index, size, DealFilterSpecifications.Suspended, cancellationToken);

            return Ok(deals);
        }

        [Authorize]
        [HttpGet("suspendedDealsCount")]
        public async Task<IActionResult> GetSuspendedDealsCountAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var dealsCount = await _dealService.GetFilteredDealsCountAsync(userId, DealFilterSpecifications.Suspended, cancellationToken);

            return Ok(dealsCount);
        }

        [Authorize]
        [HttpGet("successDealsChunk")]
        public async Task<IActionResult> GetSuccessDealsChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var deals = await _dealService.GetFilteredChunkAsync(userId, index, size, DealFilterSpecifications.Success, cancellationToken);

            return Ok(deals);
        }

        [Authorize]
        [HttpGet("successDealsCount")]
        public async Task<IActionResult> GetSuccessDealsCountAsync(CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            var dealsCount = await _dealService.GetFilteredDealsCountAsync(userId, DealFilterSpecifications.Success, cancellationToken);

            return Ok(dealsCount);
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
