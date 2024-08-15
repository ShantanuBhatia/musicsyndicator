using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "https://trylineup.tech" }, "Spotify");
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
            Console.WriteLine("Auth failed, returning login URL");
            var authorizationEndpoint = "https://accounts.spotify.com/authorize";
            var clientId = _configuration["Spotify:ClientId"];
            var redirectUri = "/signin-spotify-auth";
            var scope = "user-read-private user-read-email playlist-modify-public playlist-modify-private"; // Add other scopes as needed

            var authorizeUrl = $"{authorizationEndpoint}?client_id={clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope={Uri.EscapeDataString(scope)}";

            return Ok(new { isAuthenticated = false, loginUrl = authorizeUrl });


        }
    }
}
