using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Message)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.MessageId).OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Profile)
            .WithMany(x => x.ProfilePictures)
            .HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.ClientSetNull);
    }
}