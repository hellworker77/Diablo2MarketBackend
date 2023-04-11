using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class ItemAttributeMapper
{
    public partial ItemAttributeDto Map(ItemAttribute itemAttribute);
    public partial IList<ItemAttributeDto> MapList(IList<ItemAttribute> items);
    public partial ItemAttribute ReverseMap(ItemAttributeDto itemAttributeDto);
    public partial IList<ItemAttribute> ReverseMapList(IList<ItemAttributeDto> items);
}