namespace Entities;

public class Message
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public string InnerText { get; set; }
    public virtual Discussion Discussion { get; set; }
    public Guid DiscussionId { get; set; }
    public virtual ApplicationUser Sender { get; set; }
    public Guid SenderId { get; set; }
#pragma warning restore CS8618
    public virtual List<Media>? Attachments { get; set; }
}