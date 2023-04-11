using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities;

namespace Dal.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser,
    IdentityRole<Guid>,
    Guid,
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    public DbSet<ItemAttribute> ItemAttributes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Discussion> Discussions { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<DealMember> DealMembers { get; set; }
    public DbSet<Media> Medias { get; set; }
#pragma warning disable CS8618
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
#pragma warning restore CS8618
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}