using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Common.Services.Interfaces;
using Dal.Interfaces;

namespace Common.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemAttributeRepository _itemAttributeRepository;
    private readonly ItemMapper _mapper;
    public ItemService(IItemRepository itemRepository,
        IItemAttributeRepository itemAttributeRepository,
        ItemMapper mapper)
    {
        _itemRepository = itemRepository;
        _itemAttributeRepository = itemAttributeRepository;
        _mapper = mapper;
    }

    public async Task<ItemDto> GetByIdAsync(Guid itemId,
        CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, cancellationToken);
        if (item == null)
        {
            throw new NullReferenceException($"Item with id {itemId} not found");
        }

        return _mapper.Map(item);
    }

    public async Task<IList<ItemDto>> GetChunkOrderByPostedDateAsync(int index, 
        int size, 
        CancellationToken cancellationToken)
    {
        var items = await _itemRepository.GetChunkOrderByPostedDateAsync(index, size, cancellationToken);
        var itemsDto = _mapper.MapList(items);

        return itemsDto;
    }

    public async Task<IList<ItemDto>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken)
    {
        var items = await _itemRepository.GetChunkAsync(index, size, cancellationToken);
        if (items.Count == 0)
        {
            throw new NullReferenceException("Items in that area not found");
        }

        return _mapper.MapList(items);
    }

    public async Task<IList<ItemDto>> GetUserChunkAsync(Guid userId, 
        int index, 
        int size, 
        CancellationToken cancellationToken)
    {
        var items = await _itemRepository.GetUserChunkAsync(userId, index, size, cancellationToken);
        if (items.Count == 0)
        {
            throw new NullReferenceException("Items in that area not found");
        }

        return _mapper.MapList(items);
    }

    public async Task AddAsync(ItemDto itemDto,
        CancellationToken cancellationToken)
    {
        var item = _mapper.ReverseMap(itemDto);

        item.PostedDate = DateTime.Now.ToUniversalTime();

        await _itemRepository.AddAsync(item, cancellationToken);

        if (item.Attributes != null)
        {
            foreach (var attribute in item.Attributes)
            {
                attribute.ItemId = item.Id;
                await _itemAttributeRepository.AddAsync(attribute, cancellationToken);
            }
        }
    }

    public async Task EditAsync(ItemDto itemDto,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var validItem = await GetByIdAsync(itemDto.Id, cancellationToken);
        if (userId != validItem.OwnerId)
        {
            throw new PermissionDeniedException();
        }

        var item = _mapper.ReverseMap(itemDto);

        await _itemRepository.EditAsync(item, cancellationToken);
    }

    public async Task RaiseAsync(Guid itemId, 
        Guid userId,
        CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(itemId, cancellationToken);

        if (item == null)
        {
            throw new NullReferenceException($"Item with id {itemId} not found");
        }

        if (item.OwnerId != userId)
        {
            throw new PermissionDeniedException();
        }

        var dateNow = DateTime.Now.ToUniversalTime();

        var timeDifference = dateNow.Subtract(item.PostedDate);

        var minimumTimeAvailable = TimeSpan.FromHours(3);

        if (timeDifference < minimumTimeAvailable)
        {
            var timeAvailable = minimumTimeAvailable.Subtract(timeDifference);

            var dateAvailable = new DateTime(timeAvailable.Ticks);

            throw new ArgumentException($"The option is not available to you yet" +
                                        $"\nIt will be available through {dateAvailable.ToString("HH:mm:ss")!}");
        }

        item.PostedDate = dateNow;
        await _itemRepository.EditAsync(item, cancellationToken);
    }

    public async Task DeleteAsync(Guid itemId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var item = await GetByIdAsync(itemId, cancellationToken);
        if (item.OwnerId != userId)
        {
            throw new PermissionDeniedException();
        }

        await _itemRepository.DeleteAsync(itemId, cancellationToken);
    }
}