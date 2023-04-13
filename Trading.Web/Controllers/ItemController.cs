using Common.Models;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IIdentityService _identityService;

        public ItemController(IItemService itemService, IIdentityService identityService)
        {
            _itemService = itemService;
            _identityService = identityService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var itemDto = await _itemService.GetByIdAsync(itemId, cancellationToken);

            return Ok(itemDto);
        }

        [HttpGet("chunkOrderByPostedDate")]
        public async Task<IActionResult> GetChunkOrderByPostedDateAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var itemsDto = await _itemService.GetChunkOrderByPostedDateAsync(index, size, cancellationToken);

            return Ok(itemsDto);        
        }

        [HttpGet("chunk")]
        public async Task<IActionResult> GetChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var itemsDto = await _itemService.GetChunkAsync(index, size, cancellationToken);

            return Ok(itemsDto);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(ItemDto itemDto,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            itemDto.OwnerId = userId;

            await _itemService.AddAsync(itemDto, cancellationToken);

            return Ok("Item added");
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> EditAsync(ItemDto itemDto,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemService.EditAsync(itemDto, userId, cancellationToken);

            return Ok("Item edited");
        }

        [Authorize]
        [HttpPut("raise")]
        public async Task<IActionResult> RaiseAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemService.RaiseAsync(itemId, userId, cancellationToken);

            return Ok("Item raised");
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemService.DeleteAsync(itemId, userId, cancellationToken);

            return Ok("Item deleted");
        }
    }
}
