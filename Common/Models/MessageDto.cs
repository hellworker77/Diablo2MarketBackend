using Entities;

namespace Common.Models;

public class MessageDto
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public string InnerText { get; set; }
    public Guid DiscussionId { get; set; }
    public virtual ApplicationUserDto Sender { get; set; }
#pragma warning restore CS8618
    public virtual List<Guid>? AttachmentsId { get; set; }
}