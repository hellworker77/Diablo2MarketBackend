using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Test(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = Ok("user created");

            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, "!QAZ2wsx");
            if (!result.Succeeded)
            {
                response = BadRequest("canceled");
            }

            return response;
        } 
    }
}
