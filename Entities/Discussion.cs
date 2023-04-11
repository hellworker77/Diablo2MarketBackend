namespace Entities;

public class Discussion
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual List<ApplicationUser> Members { get; set; }
    public virtual List<Message>? Messages { get; set; }
    public virtual Deal? Deal { get; set; }
#pragma warning restore CS8618

}