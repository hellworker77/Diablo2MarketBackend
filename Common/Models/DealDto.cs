using Entities;
using Entities.Enums;

namespace Common.Models;

public class DealDto
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual List<DealMemberDto> DealMembers { get; set; }
    public virtual DiscussionDto? Discussion { get; set; }
    public virtual ItemDto Goods { get; set; }
    public DealStatus Status { get; set; }
    public virtual DateTime TransactionDate { get; set; }
#pragma warning restore CS8618 
}