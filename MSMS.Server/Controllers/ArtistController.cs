using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Helpers;
using MSMS.Server.Interfaces;
using MSMS.Server.Mappers;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        private readonly SpotifyClientBuilder _spotifyClientBuilder;

        public ArtistController(IArtistRepository artistrepo, SpotifyClientBuilder spotifyClientBuilder)
        {
            _artistRepo = artistrepo;
            _spotifyClientBuilder = spotifyClientBuilder;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnown()
        {
            var allArtists = await _artistRepo.GetAllKnownArtistsAsync();
            var allArtistDtos = allArtists.Select(at => at.ToArtistDto());
            return Ok(allArtistDtos);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> SearchByName([FromRoute] String name)
        {
            var searchResults = await _artistRepo.SearchServiceByNameAsync(name);
            var searchResultDtos = searchResults.Select(at => at.ToArtistDto());
            return Ok(searchResultDtos);
        }
    }
}
