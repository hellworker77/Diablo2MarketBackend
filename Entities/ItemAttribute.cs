namespace Entities;

public class ItemAttribute
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public string Name { get; set; }
    public string Description { get; set; }
#pragma warning restore CS8618
    public Guid ItemId { get; set; }
    public virtual Item? Item { get; set; }
}