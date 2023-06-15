using Common.Models;
using Microsoft.AspNetCore.Identity;

namespace Common.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserDto> GetByIdAsync(Guid userId);
        Task EncreaseBalanceAsync(Guid userId, 
            uint amount);
        Task<IdentityResult> UpdateAsync(Guid userId,
            string username,
            string email,
            CancellationToken cancellationToken);
        Task<IdentityResult> ChangePasswordAsync(Guid userId,
            string newPassword,
            string oldPassword,
            CancellationToken cancellationToken);
        Task<IdentityResult> RegisterAsync(string username,
            string email,
            string password);
        Task<IdentityResult> RegisterRoleAsync(string roleName);
    }
}
