namespace Common.Models;

public class ItemAttributeDto
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public string Name { get; set; }
    public string Description { get; set; }
#pragma warning restore CS8618
}