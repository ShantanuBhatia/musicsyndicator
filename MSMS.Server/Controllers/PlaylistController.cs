using Microsoft.AspNetCore.Mvc;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/playlists")]
    public class PlaylistController : ControllerBase
    {
        private static readonly string[] Playlists = new[]
        {
            "4th Gen Girl Groups", "80s Rock", "2010s Kpop", "Bollywood Party Hits"
        };
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(ILogger<PlaylistController> logger)
        {

            _logger = logger;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetPlaylistById([FromRoute] int id)
        {
            if (id == 999)
            {
                return NotFound();
            }
            int PlaylistToFetch = id % Playlists.Length;

            var testPlaylist = new SpotifyPlaylist
            {
                SpotifyPlaylistId = PlaylistToFetch,
                SpotifyPlaylistName = Playlists[PlaylistToFetch],
                UserId= 1234
            };

            return new JsonResult(testPlaylist);
        }
    }
}
