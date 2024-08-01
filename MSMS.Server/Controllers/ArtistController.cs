using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Interfaces;
using MSMS.Server.Mappers;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepo;
        public ArtistController(IArtistRepository artistrepo)
        {
            _artistRepo = artistrepo;
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
