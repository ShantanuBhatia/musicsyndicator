using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using MSMS.Server.Helpers;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SpotifyClientBuilder _spotifyClientBuilder;

        public SearchController(SpotifyClientBuilder spotifyClientBuilder)
        {
            _spotifyClientBuilder = spotifyClientBuilder;
        }


        [HttpGet("artists")]
        [Authorize]
        public async Task<IActionResult> SearchArtists(string query)
        {
            try
            {

                var expiresAt = await HttpContext.GetTokenAsync("expires_at");
                await Console.Out.WriteLineAsync($"########## Token expires at {expiresAt}");
                //var accessToken = await HttpContext.GetTokenAsync("access_token");
                //var spotify = new SpotifyClient(accessToken);
                var spotify = await _spotifyClientBuilder.BuildClient();
                var searchResponse = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, query));
                
                return Ok(searchResponse.Artists.Items);
            }
            catch (APIException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Response?.StatusCode);

                return StatusCode(500, "Error occurred");
            }
        }
    }
}
