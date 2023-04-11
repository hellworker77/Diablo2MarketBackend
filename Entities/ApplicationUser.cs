using Microsoft.AspNetCore.Identity;

namespace Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public uint Balance { get; set; }
    public virtual List<Media>? ProfilePictures { get; set; }
    public virtual List<DealMember>? DealMembers { get; set; }
    public virtual List<Discussion>? Discussions { get; set; }
    public virtual List<Item>? Items { get; set; }
    public virtual List<Message>? Messages { get; set; }
}