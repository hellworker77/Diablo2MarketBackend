using Entities;

namespace Common.Models;

public class DiscussionDto
{
    public Guid Id { get; set; }
#pragma warning disable CS8618
    public virtual List<ApplicationUserDto> Members { get; set; }
#pragma warning restore CS8618
}