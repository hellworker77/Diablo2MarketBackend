using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;


[Mapper]
public partial class DealMapper
{
    [MapperIgnoreSource(nameof(Deal.Discussion.Deal))]
    [MapperIgnoreSource(nameof(Deal.Discussion.Messages))]
    public partial DealDto Map(Deal deal);
    [MapperIgnoreSource(nameof(Deal.Discussion.Deal))]
    [MapperIgnoreSource(nameof(Deal.Discussion.Messages))]
    public partial IList<DealDto> MapList(IList<Deal> deals);
    public partial Deal ReverseMap(DealDto deal);
    public partial IList<Deal> ReverseMapList(IList<DealDto> deals);
}
