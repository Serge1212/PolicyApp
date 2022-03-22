using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyApp.Core.DTOs.User;
using PolicyApp.Core.Services.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace PolicyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            return Ok(_userService.AuthenticateUser(userLogin));
        }

        [Authorize]
        [HttpGet("my-email")]
        public string GetMyEmail()
        {
            var email = User.Claims.First(c => c.Type == ClaimTypes.Email);
            return email.Value;
        }
    }
}
