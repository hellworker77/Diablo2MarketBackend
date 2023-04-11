using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace User.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            IActionResult response;

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                response = BadRequest(result.ToString());
            }
            else
            {
                response = Ok(result.ToString());
            }

            return response;
        }

        [HttpPost("role")]
        public async Task<IActionResult> RegisterRole(string roleName)
        {
            IActionResult response;

            var role = new IdentityRole<Guid>("admin");

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                response = BadRequest(result.ToString());
            }
            else
            {
                response = Ok(result.ToString());
            }

            return response;
        }
    }
}
