using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers
{
    [Mapper]
    public partial class ApplicationUserMapper
    {
        [MapperIgnoreSource(nameof(ApplicationUser.Discussions))]
        [MapperIgnoreSource(nameof(ApplicationUser.DealMembers))]
        [MapperIgnoreSource(nameof(ApplicationUser.Messages))]
        public partial ApplicationUserDto Map(ApplicationUser applicationUser);
        public partial ApplicationUser ReverseMap(ApplicationUserDto applicationUserDto);
    }
}
