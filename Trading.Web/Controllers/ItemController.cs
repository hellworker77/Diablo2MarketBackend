using Common.Models;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var itemDto = await _itemService.GetByIdAsync(itemId, cancellationToken);

            return Ok(itemDto);
        }

        [HttpGet("chunk")]
        public async Task<IActionResult> GetChunkAsync(int index,
            int size,
            CancellationToken cancellationToken)
        {
            var itemsDto = await _itemService.GetChunkAsync(index, size, cancellationToken);

            return Ok(itemsDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(ItemDto itemDto,
            CancellationToken cancellationToken)
        {
            await _itemService.AddAsync(itemDto, cancellationToken);

            return Ok("Item added");
        }
        [HttpPut("edit")]
        public async Task<IActionResult> EditAsync(ItemDto itemDto,
            CancellationToken cancellationToken)
        {
            await _itemService.EditAsync(itemDto, cancellationToken);

            return Ok("Item edited");
        }
        [HttpPut("raise")]
        public async Task<IActionResult> RaiseAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            await _itemService.RaiseAsync(itemId, cancellationToken);

            return Ok("Item raised");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            await _itemService.DeleteAsync(itemId, cancellationToken);

            return Ok("Item deleted");
        }
    }
}
