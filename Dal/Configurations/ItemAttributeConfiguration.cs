using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations;

public class ItemAttributeConfiguration : IEntityTypeConfiguration<ItemAttribute>
{
    public void Configure(EntityTypeBuilder<ItemAttribute> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Item)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.ItemId);
    }
}