using Entities.Enums;
using Entities;

namespace Common.Models;

public class ItemDto
{
    public Guid Id { get; set; }
    public uint Price { get; set; }
    public Guid? DealId { get; set; }
    public ItemRarity Rarity { get; set; }
    public virtual List<ItemAttributeDto>? Attributes { get; set; }
#pragma warning disable CS8618
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
#pragma warning restore CS8618
    public DateTime PostedDate { get; set; }
}