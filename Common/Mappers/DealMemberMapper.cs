using Common.Models;
using Entities;
using Riok.Mapperly.Abstractions;

namespace Common.Mappers;

[Mapper]
public partial class DealMemberMapper
{
    [MapperIgnoreSource(nameof(DealMember.User))]
    [MapperIgnoreSource(nameof(DealMember.Deal))]
    public partial DealMemberDto Map(DealMember dealMember);
    public partial DealMember ReverseMap(DealMemberDto dealMemberDto);
    public partial IList<DealMemberDto> MapList(IList<DealMember> dealMember);
}