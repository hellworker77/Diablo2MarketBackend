using System.Security.Cryptography.X509Certificates;
using Entities.Enums;

namespace Entities;

public class Deal
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual List<DealMember> DealMembers { get; set; }
    public virtual Discussion? Discussion { get;set; }
    public Guid? DiscussionId { get; set; }
    public virtual Item Goods { get; set; }
    public Guid GoodsId { get; set; }
    public DealStatus Status { get; set; }
    public DateTime UpdatedStatusTime { get; set; }
    public DateTime TransactionDate { get; set; }
#pragma warning restore CS8618 
}