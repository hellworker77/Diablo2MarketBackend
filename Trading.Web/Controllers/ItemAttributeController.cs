using Common.Models;
using Common.Services;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAttributeController : ControllerBase
    {
        private readonly IItemAttributeService _itemAttributeService;
        private readonly IIdentityService _identityService;

        public ItemAttributeController(IItemAttributeService itemAttributeService, IIdentityService identityService)
        {
            _itemAttributeService = itemAttributeService;
            _identityService = identityService;
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

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(Guid itemId,
            ItemAttributeDto itemAttributeDto,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemAttributeService.AddToItemAsync(itemId, userId, itemAttributeDto, cancellationToken);

            return Ok("Item Attribute added");
        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> EditAsync(Guid itemId, ItemAttributeDto itemAttributeDto,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemAttributeService.EditAsync(itemId, itemAttributeDto, userId, cancellationToken);

            return Ok("Item Attribute edited");
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Guid itemId,
            Guid itemAttributeId,
            CancellationToken cancellationToken)
        {
            var userId = _identityService.GetUserIdentity();
            await _itemAttributeService.DeleteAsync(itemId, itemAttributeId, userId, cancellationToken);

            return Ok("Item Attribute deleted");
        }
    }
}
