using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dal.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OwnerId);
        builder.HasOne(x => x.Deal)
            .WithOne(x => x.Goods)
            .HasForeignKey<Deal>(x => x.GoodsId).OnDelete(DeleteBehavior.Cascade);
    }
}