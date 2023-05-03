using Common.Models;
using Common.Services.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Account.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserDto> GetByIdAsync(Guid userId);
        Task EncreaseBalanceAsync(Guid userId, 
            uint amount);
        Task<IdentityResult> RegisterAsync(string username,
            string email,
            string password);
        Task<IdentityResult> RegisterRoleAsync(string roleName);
    }
}
