using AspNet.Security.OAuth.Spotify;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace MSMS.Server.Infrastructure
{
    public class CustomSpotifyHandler : AuthenticationHandler<SpotifyAuthenticationOptions>
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public CustomSpotifyHandler(
            IOptionsMonitor<SpotifyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationSchemeProvider schemeProvider)
            : base(options, logger, encoder, clock)
        {
            _schemeProvider = schemeProvider;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Try to authenticate using the Spotify handler
            var spotifyScheme = await _schemeProvider.GetSchemeAsync("Spotify");
            if (spotifyScheme == null)
            {
                return AuthenticateResult.Fail("Spotify authentication scheme not found.");
            }

            var spotifyResult = await Context.AuthenticateAsync(spotifyScheme.Name);
            if (spotifyResult?.Succeeded ?? false)
            {
                return AuthenticateResult.Success(spotifyResult.Ticket);
            }
            return AuthenticateResult.NoResult();
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            return Response.WriteAsJsonAsync(new { message = "Unauthorized. Authentication required." });
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            return Response.WriteAsJsonAsync(new { message = "Forbidden. You do not have access to this resource." });
        }
    }
}
