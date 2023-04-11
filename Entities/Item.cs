using Entities.Enums;

namespace Entities;

public class Item
{
    public Guid Id { get; set; }
    public uint Price { get; set; }
    public virtual Deal? Deal { get; set; }
    public ItemRarity Rarity { get; set; }
    public virtual List<ItemAttribute>? Attributes { get; set; }
#pragma warning disable CS8618
    public string Name { get; set; }
    public virtual ApplicationUser Owner { get; set; }
    public Guid OwnerId { get; set; }
#pragma warning restore CS8618
    public DateTime PostedDate { get; set; }
}