using Account.Web.Services.Interfaces;
using Common.Exceptions;
using Common.Mappers;
using Common.Models;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Account.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ApplicationUserMapper _applicationUserMapper;

        public AccountService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole<Guid>> roleManager,
            ApplicationUserMapper applicationUserMapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationUserMapper = applicationUserMapper;
        }
        
        public async Task<ApplicationUserDto> GetMeAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), userId);
            }

            return _applicationUserMapper.Map(user);
        }

        public async Task EncreaseBalanceAsync(Guid userId, 
            uint amount)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new EntityNotFoundException(typeof(ApplicationUser), userId);
            }

            user.Balance += amount;
            var updatingResult = await _userManager.UpdateAsync(user);
            if (!updatingResult.Succeeded)
            {
                throw new ArgumentException("Failed to update balance");
            }
        }

        public async Task<IdentityResult> RegisterAsync(string username, 
            string email, 
            string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> RegisterRoleAsync(string roleName)
        {
            var role = new IdentityRole<Guid>(roleName);

            return await _roleManager.CreateAsync(role);
        }
    }
}
