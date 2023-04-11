using Entities.Enums;
using Entities;

namespace Common.Models;

public class DealMemberDto
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual Guid UserId { get; set; }
    public virtual Guid DealId { get; set; }
    public bool Approved { get; set; }
    public DealMemberStatus Status { get; set; }
#pragma warning restore CS8618
}