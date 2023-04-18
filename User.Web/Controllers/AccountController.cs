using Account.Web.Services.Interfaces;
using Common.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IIdentityService _identityService;

        public AccountController(IAccountService accountService,
            IIdentityService identityService)
        {
            _accountService = accountService;
            _identityService = identityService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMeAsync()
        {
            var userId = _identityService.GetUserIdentity();
            var user = await _accountService.GetByIdAsync(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid userId)
        {
            var user = await _accountService.GetByIdAsync(userId);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("encreaseBalance")]
        public async Task<IActionResult> EncreaseBalanceAsync(uint amount)
        {
            var userId = _identityService.GetUserIdentity();
            await _accountService.EncreaseBalanceAsync(userId, amount);

            return Ok("Balance has been increased");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(string username, 
            string email, 
            string password)
        {
            var result = await _accountService.RegisterAsync(username, email, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }
            else
            {
                return Ok(result.ToString());
            }
        }

        [HttpPost("role")]
        public async Task<IActionResult> RegisterRoleAsync(string roleName)
        {
            var result = await _accountService.RegisterRoleAsync(roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.ToString());
            }
            else
            {
                return Ok(result.ToString());
            }
        }
    }
}
