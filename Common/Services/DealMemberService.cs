using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Common.Services.Interfaces;
using Dal.Interfaces;
using Entities;
using Entities.Enums;

namespace Common.Services;

public class DealMemberService : IDealMemberService
{
    private readonly IDealMemberRepository _dealMemberRepository;
    private readonly DealMemberMapper _dealMemberMapper;

    public DealMemberService(IDealMemberRepository dealMemberRepository,
        DealMemberMapper dealMemberMapper)
    {
        _dealMemberRepository = dealMemberRepository;
        _dealMemberMapper = dealMemberMapper;
    }

    public async Task CreateAsync(Guid dealId,
        Guid userId,
        DealMemberStatus status,
        CancellationToken cancellationToken)
    {
        var dealMember = new DealMember
        {
            DealId = dealId,
            UserId = userId,
            Status = status,
        };

        await _dealMemberRepository.CreateAsync(dealMember, cancellationToken);
    }

    public async Task<IList<DealMemberDto>> GetByDealIdAsync(Guid dealId,
        CancellationToken cancellationToken)
    {
        var dealMembers = await _dealMemberRepository.GetByDealIdAsync(dealId, cancellationToken);

        if (dealMembers.Count == 0)
        {
            throw new NullReferenceException("DealMembers in that deal not found");
        }

        return _dealMemberMapper.MapList(dealMembers);
    }

    public async Task ApproveAsync(Guid dealMemberId,
        CancellationToken cancellationToken)
    {
        var dealMember = await _dealMemberRepository.GetByIdAsync(dealMemberId, cancellationToken);

        if (dealMember == null)
        {
            throw new EntityNotFoundException(typeof(DealMember), dealMemberId);
        }

        dealMember.Approved = true;

        await _dealMemberRepository.ApproveAsync(dealMember, cancellationToken);
    }
}