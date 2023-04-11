using Entities.Enums;
using Entities;

namespace Common.Models;

public class MediaDto
{
    public Guid Id { get; set; }
    public MediaType Type { get; set; }
#pragma warning disable CS8618
    public byte[] Data { get; set; }
#pragma warning restore CS8618
}