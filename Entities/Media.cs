using Entities.Enums;

namespace Entities;

public class Media
{
    public Guid Id { get; set; }
    public MediaType Type { get; set; }
#pragma warning disable CS8618
    public byte[] Data { get; set; }
    public Guid? MessageId { get; set; }
    public virtual Message? Message { get; set; }
    public Guid? ProfileId { get; set; }
    public virtual ApplicationUser? Profile { get; set; }
#pragma warning restore CS8618
}