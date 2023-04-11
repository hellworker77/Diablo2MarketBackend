using Entities.Enums;

namespace Entities;

public class DealMember
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual ApplicationUser User { get; set; }
    public Guid UserId { get; set; }
    public virtual Deal Deal { get; set; }
    public Guid DealId { get; set; }
    public bool Approved { get; set; }
    public DealMemberStatus Status { get; set; }
#pragma warning restore CS8618
}