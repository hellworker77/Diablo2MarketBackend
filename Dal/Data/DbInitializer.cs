using Dal.Interfaces;

namespace Dal.Data;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationContext _context;

    public DbInitializer(ApplicationContext context)
    {
        _context = context;
    }
    public void Initialize()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Roles.AddRange(FakeData.Roles);
        _context.SaveChanges();

        _context.Users.AddRange(FakeData.Users);
        _context.SaveChanges();

        _context.UserRoles.AddRange(FakeData.UserRoles);
        _context.SaveChanges();

        _context.Items.AddRange(FakeData.Items);
        _context.SaveChanges();

        _context.ItemAttributes.AddRange(FakeData.ItemAttributes);
        _context.SaveChanges();

        _context.Discussions.AddRange(FakeData.Discussions);
        _context.SaveChanges();

        _context.Deals.AddRange(FakeData.Deals);
        _context.SaveChanges();

        _context.Messages.AddRange(FakeData.Messages);
        _context.SaveChanges();

        _context.Medias.AddRange(FakeData.Medias);
        _context.SaveChanges();

        _context.DealMembers.AddRange(FakeData.DealMembers);
        _context.SaveChanges();
    }
}