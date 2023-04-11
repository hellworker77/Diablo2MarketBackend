using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Dal.Data;

public static class FakeData
{
    private static Guid DiscussionId = Guid.NewGuid();

    public static ICollection<IdentityRole<Guid>> Roles = new List<IdentityRole<Guid>>
    {
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "admin",
            NormalizedName = "ADMIN"
        }
    };
    public static ICollection<ApplicationUser> Users = new List<ApplicationUser>
    {
        new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Balance = 10,
            Email = "admin@aaa.com",
            NormalizedEmail = "ADMIN@AAA.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAIAAYagAAAAEOObrxK8MEi9CAr6V1lm3CjQwpdMWO46J15/fN4AshwLh45ThOxSLoOFh1id4JNFQA==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D")
        },
        new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Balance = 10,
            Email = "user@aaa.com",
            NormalizedEmail = "USER@AAA.COM",
            UserName = "user",
            NormalizedUserName = "USER",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAIAAYagAAAAEOObrxK8MEi9CAr6V1lm3CjQwpdMWO46J15/fN4AshwLh45ThOxSLoOFh1id4JNFQA==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D")
        }
    };
    public static ICollection<IdentityUserRole<Guid>> UserRoles = new List<IdentityUserRole<Guid>>
    {
        new IdentityUserRole<Guid>
        {
            UserId = Users.First().Id,
            RoleId = Roles.First().Id
        }
    };
    public static ICollection<Item> Items = new List<Item>
    {
        new Item
        {
            Price = 20,
            Id = Guid.NewGuid(),
            OwnerId = Users.Last().Id,
            Name = "Harlequin Crest",
            Rarity = ItemRarity.Legend,
            PostedDate = DateTime.Now.ToUniversalTime()
        },
        new Item
        {
            Price = 20,
            Id = Guid.NewGuid(),
            OwnerId = Users.Last().Id,
            Name = "Sword",
            Rarity = ItemRarity.Legend,
            PostedDate = DateTime.Now.ToUniversalTime()
        }
    };
    public static ICollection<ItemAttribute> ItemAttributes = new List<ItemAttribute>
    {
        new ItemAttribute
        {
            Description = "Damage Reduce By 10%",
            Name = "Attr",
            ItemId = Items.First().Id,
        },
        new ItemAttribute
        {
            Description = "+2 To All Skills",
            Name = "Attr",
            ItemId = Items.First().Id,
        },
        new ItemAttribute
        {
            Description = "+1-148 To Life (+1.5 Per Character Level)",
            Name = "Attr",
            ItemId = Items.First().Id,
        },
        new ItemAttribute
        {
            Description = "+1-148 To Mana (+1.5 Per Character Level)",
            Name = "Attr",
            ItemId = Items.First().Id,
        },
        new ItemAttribute
        {
            Description = "50% Better Chance of Getting Magic Items",
            Name = "Attr",
            ItemId = Items.First().Id,
        },
        new ItemAttribute
        {
            Description = "+2 To All ItemAttributes",
            Name = "Attr",
            ItemId = Items.First().Id,
        }
    };

    public static ICollection<Deal> Deals = new List<Deal>()
    {
        new Deal()
        {
            Id = Guid.NewGuid(),
            DiscussionId = DiscussionId,
            GoodsId = Items.First().Id
        }
    };
    public static ICollection<Discussion> Discussions = new List<Discussion>
    {
        new Discussion
        {
            Id = DiscussionId,
        }
    };
    

    public static ICollection<Message> Messages = new List<Message>
    {
        new Message
        {
            Id = Guid.NewGuid(),
            InnerText = "Suck my dick",
            DiscussionId = DiscussionId,
            SenderId = Users.First().Id
        }
    };
    public static ICollection<Media> Medias = new List<Media>
    {
        new Media
        {
            MessageId = Messages.First().Id,
            Data = Array.Empty<byte>(),
            Type = MediaType.Video
        },
        new Media
        {
            ProfileId = Users.First().Id,
            Data = Array.Empty<byte>(),
            Type = MediaType.Photo
        }
    };

    public static ICollection<DealMember> DealMembers = new List<DealMember>
    {
        new DealMember
        {
            DealId = Deals.First().Id,
            Status = DealMemberStatus.Seller,
            UserId = Users.First().Id
        }
    };
}