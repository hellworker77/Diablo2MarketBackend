using Common.Models;
using Common.Services;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAttributeController : ControllerBase
    {
        private readonly IItemAttributeService _itemAttributeService;

        public ItemAttributeController(IItemAttributeService itemAttributeService)
        {
            _itemAttributeService = itemAttributeService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid itemAttributeId,
            CancellationToken cancellationToken)
        {
            var itemAttributeDto = await _itemAttributeService.GetByIdAsync(itemAttributeId, cancellationToken);

            return Ok(itemAttributeDto);
        }
        [HttpGet("itemId")]
        public async Task<IActionResult> GetChunkAsync(Guid itemId,
            CancellationToken cancellationToken)
        {
            var itemAttributesDto = await _itemAttributeService.GetFromItemAsync(itemId, cancellationToken);

            return Ok(itemAttributesDto);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Guid itemId,
            ItemAttributeDto itemAttributeDto,
            CancellationToken cancellationToken)
        {
            await _itemAttributeService.AddToItemAsync(itemId, itemAttributeDto, cancellationToken);

            return Ok("Item Attribute added");
        }
        [HttpGet("edit")]
        public async Task<IActionResult> EditAsync(ItemAttributeDto itemAttributeDto,
            CancellationToken cancellationToken)
        {
            await _itemAttributeService.EditAsync(itemAttributeDto, cancellationToken);

            return Ok("Item Attribute edited");
        }
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteAsync(Guid itemAttributeId,
            CancellationToken cancellationToken)
        {
            await _itemAttributeService.DeleteAsync(itemAttributeId, cancellationToken);

            return Ok("Item Attribute deleted");
        }
    }
}
