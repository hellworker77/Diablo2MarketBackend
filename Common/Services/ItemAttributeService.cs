using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Common.Services.Interfaces;
using Dal.Interfaces;
using Entities;

namespace Common.Services;

public class ItemAttributeService : IItemAttributeService
{
    private readonly IItemAttributeRepository _itemAttributeRepository;
    private readonly IItemService _itemService;
    private readonly ItemAttributeMapper _mapper;

    public ItemAttributeService(IItemAttributeRepository itemAttributeRepository,
        IItemService itemService,
        ItemAttributeMapper mapper)
    {
        _itemAttributeRepository = itemAttributeRepository;
        _itemService = itemService;
        _mapper = mapper;
    }

    public async Task<ItemAttributeDto> GetByIdAsync(Guid itemAttributeId,
        CancellationToken cancellationToken)
    {
        var itemAttribute = await _itemAttributeRepository
            .GetByIdAsync(itemAttributeId, cancellationToken);

        if (itemAttribute == null)
        {
            throw new EntityNotFoundException(typeof(ItemAttribute), itemAttributeId);
        }

        return _mapper.Map(itemAttribute);
    }

    public async Task<IList<ItemAttributeDto>> GetFromItemAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        var itemAttributes = await _itemAttributeRepository.GetFromItemAsync(itemId, cancellationToken);

        if (itemAttributes.Count == 0)
        {
            throw new EntityNotFoundException(typeof(Item), itemId, typeof(ItemAttribute));

        }

        return _mapper.MapList(itemAttributes);
    }

    public async Task AddToItemAsync(Guid itemId,
        Guid userId,
        ItemAttributeDto itemAttributeDto,
        CancellationToken cancellationToken)
    {
        var item = await _itemService.GetByIdAsync(itemId, cancellationToken);
        if (item.OwnerId != userId)
        {
            throw new PermissionDeniedException();
        }

        var itemAttribute = _mapper.ReverseMap(itemAttributeDto);

        itemAttribute.ItemId = itemId;

        await _itemAttributeRepository.AddAsync(itemAttribute, cancellationToken);
    }

    public async Task EditAsync(Guid itemId, ItemAttributeDto itemAttributeDto,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var item = await _itemService.GetByIdAsync(itemId, cancellationToken);
        if (item.OwnerId != userId)
        {
            throw new PermissionDeniedException();
        }

        var itemAttribute = _mapper.ReverseMap(itemAttributeDto);

        await _itemAttributeRepository.EditAsync(itemAttribute, cancellationToken);
    }

    public async Task DeleteAsync(Guid itemId,
        Guid itemAttributeId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var item = await _itemService.GetByIdAsync(itemId, cancellationToken);
        if (item.OwnerId != userId)
        {
            throw new PermissionDeniedException();
        }

        await _itemAttributeRepository.DeleteAsync(itemAttributeId, cancellationToken);
    }
}