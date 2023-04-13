using Entities;

namespace Common.Models;

public class ApplicationUserDto
{
    public uint Balance { get; set; }
    public virtual List<Guid>? ProfilePictures { get; set; }
    public virtual List<ItemDto>? Items { get; set; }
}