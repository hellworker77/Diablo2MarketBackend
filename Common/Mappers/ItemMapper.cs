using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class ItemMapper
{
    public partial ItemDto Map(Item item);
    public partial IList<ItemDto> MapList(IList<Item> items);
    public partial Item ReverseMap(ItemDto item);
    public partial IList<Item> ReverseMapList(IList<ItemDto> items);
}