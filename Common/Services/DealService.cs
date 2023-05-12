using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Common.Services.Interfaces;
using Dal.Interfaces;
using Dal.Repositories;
using Entities;
using Entities.Enums;
using Filters.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Common.Services;

public class DealService : IDealService
{
    private readonly IDealMemberService _dealMemberService;
    private readonly IDealRepository _dealRepository;
    private readonly IItemService _itemService;
    private readonly DealMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public DealService(IDealRepository dealRepository,
        IItemService itemService,
        IDealMemberService dealMemberService,
        UserManager<ApplicationUser> userManager,
        DealMapper mapper)
    {
        _dealRepository = dealRepository;
        _itemService = itemService;
        _dealMemberService = dealMemberService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<DealDto> GetByIdAsync(Guid dealId,
        CancellationToken cancellationToken)
    {
        var deal = await _dealRepository
            .GetByIdAsync(dealId, cancellationToken);

        if (deal == null)
        {
            throw new EntityNotFoundException(typeof(Deal), dealId);
        }

        return _mapper.Map(deal);
    }

    public async Task<IList<DealDto>> GetFilteredChunkAsync(int index, 
        int size, 
        AbstractFilterSpecification<Deal> abstractFilterSpecification, 
        CancellationToken cancellationToken)
    {
        var deals = await _dealRepository.GetFilteredChunkAsync(index, size, abstractFilterSpecification, cancellationToken);
        if (deals is null)
        {
            throw new NullReferenceException("Deals in that area are not found");
        }

        return _mapper.MapList(deals);
    }

    public async Task<IList<DealDto>> GetUserChunkAsync(Guid userId,
        int index,
        int size,
        CancellationToken cancellationToken)
    {
        var deals = await _dealRepository.GetChunkAsync(userId, index, size, cancellationToken);
        if (deals is null)
        {
            throw new NullReferenceException("Deals in that area are not found");
        }

        return _mapper.MapList(deals);
    }

    public async Task<int> GetUserDealsCountAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var dealsCount = await _dealRepository.GetUserDealsCountAsync(userId, cancellationToken);

        return dealsCount;
    }

    public async Task<IList<DealDto>> GetChunkAsync(int index,
        int size,
        CancellationToken cancellationToken)
    {
        var deals = await _dealRepository.GetChunkAsync(index, size, cancellationToken);
        if (deals.Count == 0)
        {
            throw new NullReferenceException("Deals in that area are not found");
        }

        return _mapper.MapList(deals);
    }

    public async Task<int> GetDealsCountAsync(CancellationToken cancellationToken)
    {
        var dealsCount = await _dealRepository.GetDealsCountAsync(cancellationToken);

        return dealsCount;
    }

    public async Task CreateAsync(Guid itemId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var item = await _itemService.GetByIdAsync(itemId, cancellationToken);

        if (item.DealId != null)
        {
            throw new ArgumentException("Deal is already made");
        }

        if (item.OwnerId == userId)
        {
            throw new ArgumentException("You can't make a deal with yourself");
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new EntityNotFoundException(typeof(ApplicationUser), userId);
        }

        if (user.Balance <= item.Price)
        {
            throw new ArgumentException("You don't have enough money to make this deal");
        }

        user.Balance -= item.Price;
        var updatingResult = await _userManager.UpdateAsync(user);
        if (!updatingResult.Succeeded)
        {
            throw new ArgumentException("Failed to update balance");
        }

        var deal = new Deal
        {
            GoodsId = item.Id,
            TransactionDate = DateTime.Now.ToUniversalTime(),
            Status = DealStatus.InProgress,
            UpdatedStatusTime = DateTime.Now.ToUniversalTime()
        };

        await _dealRepository.CreateAsync(deal, cancellationToken);

        await _dealMemberService.CreateAsync(deal.Id, userId, DealMemberStatus.Buyer, cancellationToken);
        await _dealMemberService.CreateAsync(deal.Id, item.OwnerId, DealMemberStatus.Seller, cancellationToken);
    }

    public async Task ApproveAsync(Guid dealId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var dealDto = await GetByIdAsync(dealId, cancellationToken);

        var dealMemberDto = dealDto.DealMembers.FirstOrDefault(x => x.UserId == userId);

        await _dealMemberService.ApproveAsync(dealMemberDto!.Id, cancellationToken);

        dealDto = await GetByIdAsync(dealId, cancellationToken);

        if (dealDto.DealMembers.All(x => x.Approved))
        {
            dealDto.Status = DealStatus.Success;

            var deal = _mapper.ReverseMap(dealDto);
            deal.UpdatedStatusTime = DateTime.Now.ToUniversalTime();

            await _dealRepository.EditAsync(deal, cancellationToken);

            var sellerId = dealDto.DealMembers
                !.FirstOrDefault(x => x.Status == DealMemberStatus.Seller)
                !.UserId;

            var seller = await _userManager.FindByIdAsync(sellerId.ToString());
            if (seller is null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), sellerId);
            }

            seller.Balance += dealDto.Goods.Price;
            await _userManager.UpdateAsync(seller);
        }
    }

    public async Task DeleteAsync(Guid dealId,
        CancellationToken cancellationToken)
    {
        var deal = await _dealRepository.GetByIdAsync(dealId, cancellationToken);
        if (deal == null)
        {
            throw new NullReferenceException($"Deal with id:{dealId} not found");
        }

        await _dealRepository.DeleteAsync(deal, cancellationToken);
    }

    public async Task EditAsync(DealDto dealDto,
        CancellationToken cancellationToken)
    {
        var deal = _mapper.ReverseMap(dealDto);
        await _dealRepository.EditAsync(deal, cancellationToken);
    }
}