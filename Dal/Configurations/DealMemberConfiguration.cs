using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations;

public class DealMemberConfiguration : IEntityTypeConfiguration<DealMember>
{
    public void Configure(EntityTypeBuilder<DealMember> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany(x => x.DealMembers)
            .HasForeignKey(x => x.UserId);
    }
}