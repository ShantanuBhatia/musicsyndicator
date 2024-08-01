using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Models;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/artistlists")]
    public class ArtistListController : ControllerBase
    {
        private static readonly string[] TempArtists = ["Stray Kids", "RESCENE", "NewJeans", "LeSserafim"];

        private readonly ILogger<ArtistListController> _logger;

        public ArtistListController(ILogger<ArtistListController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetArtistListById([FromRoute] int id)
        {
            if (id == 999)
            {
                return NotFound();
            }

            List<Artist> artists = new List<Artist>();
            for (int i = 0; i < TempArtists.Length; i++) 
            { 
                artists.Add(new Artist { ArtistDisplayName = TempArtists[i], ArtistId=i+200});
            }
            ArtistList returnObject = new ArtistList
            {
                ArtistListName = "Example Artist List Number " + id.ToString(),
                Artists = artists,
                ArtistListId = Random.Shared.Next(-20, 55),
                UserId = 42069
            };
            return Ok(returnObject);

        }
    }
}
