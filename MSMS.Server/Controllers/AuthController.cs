using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using System.Security.Claims;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, "Spotify");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var name = User.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(new { isAuthenticated = true, name });
            }
            return Ok(new { isAuthenticated = false });
        }
    }
}
