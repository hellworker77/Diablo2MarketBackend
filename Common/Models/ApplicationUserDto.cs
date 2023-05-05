using Entities;

namespace Common.Models;

public class ApplicationUserDto
{
    public uint Balance { get; set; }
#pragma warning disable CS8618 
    public string UserName { get; set; }
    public string Email { get; set; }
#pragma warning restore CS8618
    public virtual List<MediaShortDto>? ProfilePictures { get; set; }
    public virtual List<ItemDto>? Items { get; set; }
}