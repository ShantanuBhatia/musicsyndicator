using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;

namespace MSMS.Server.Helpers
{
    public class SpotifyClientBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SpotifyClientConfig _spotifyClientConfig;
        private readonly IConfiguration _configuration;

        public SpotifyClientBuilder(IHttpContextAccessor httpContextAccessor, SpotifyClientConfig spotifyClientConfig, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _spotifyClientConfig = spotifyClientConfig;
            _configuration = configuration;
        }

        public async Task<SpotifyClient> BuildClient()
        {

            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var expiresAt = await _httpContextAccessor.HttpContext.GetTokenAsync("expires_at");

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(expiresAt))
            {
                throw new UnauthorizedAccessException("Spotify access token not found.");
            }

            if (DateTimeOffset.Parse(expiresAt) <= DateTimeOffset.UtcNow)
            {
                // Token has expired, refresh 
                await RefreshAccessTokenAsync();
                accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            }

            return new SpotifyClient(accessToken);
        }

        private async Task RefreshAccessTokenAsync()
        {
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new UnauthorizedAccessException("Refresh token not found.");
            }

            var clientId = _configuration["Spotify:ClientId"];
            var clientSecret = _configuration["Spotify:ClientSecret"];

            var response = await new OAuthClient().RequestToken(
            new AuthorizationCodeRefreshRequest(clientId, clientSecret, refreshToken));

            var authInfo = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            authInfo.Properties.UpdateTokenValue("access_token", response.AccessToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", response.RefreshToken);
            authInfo.Properties.UpdateTokenValue("expires_at",
                DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn).ToString("o"));

            await _httpContextAccessor.HttpContext.SignInAsync(authInfo.Principal, authInfo.Properties);

        }
    }
}
