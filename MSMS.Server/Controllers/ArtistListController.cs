using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Mappers;
using MSMS.Server.Models;

using MSMS.Server.Data;
using MSMS.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SpotifyAPI.Web;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/artistlists")]
    [Authorize]
    public class ArtistListController : ControllerBase
    {
        private readonly ILogger<ArtistListController> _logger;
        private readonly IArtistListRepository _artistListRepo;
        private readonly IArtistRepository _artistRepo;

        public ArtistListController(ILogger<ArtistListController> logger, ApplicationDBContext context, IArtistListRepository artistListRepo, IArtistRepository artistRepo)
        {
            _logger = logger;
            _artistListRepo = artistListRepo;
            _artistRepo = artistRepo;
        }
        
        [HttpGet("api/artistlists/admin-all")]
        public async Task<IActionResult> GetAll()
        {
            var artistLists = await _artistListRepo.GetAllAsync();
            var artistListDtos = artistLists.Select(al => al.ToArtistListDto());
            return Ok(artistListDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser()
        {
            var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(spotifyUserId))
            {
                return Unauthorized("User ID not found in the authentication token.");
            }
            
            var artistLists = await _artistListRepo.GetAllForUserAsync(spotifyUserId);
            var allArtistListDtos = artistLists.Select(al => al.ToArtistListDto());
            
            return Ok(allArtistListDtos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArtistListById([FromRoute] int id)
        {
            var artistList = await _artistListRepo.GetByIDAsync(id);
            if (artistList == null)
            {
                return NotFound();
            }
            return Ok(artistList.ToArtistListDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistListDto ListCreationData)
        {
            // TODO put an upper limit on the number of artists allowed per list
            // something reasonable like uhh 10?

            // TODO put an upper limit on number of lists per user
            // something reasonable like uhh 10?
            try
            {
                
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var spotify = new SpotifyClient(accessToken);
                var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(spotifyUserId))
                {
                    return Unauthorized("User ID not found in the authentication token.");
                }

                // Initialize an ArtistList object, load it up with all the artists, write to DB.
                var ArtistListModel = await ListCreationData.ToArtistListFromCreateDto(_artistRepo, spotifyUserId, spotify);
                foreach (var a in ArtistListModel.Artists)
                {
                    Console.WriteLine($"Spotify key: {a.ArtistSpotifyKey}, artist name {a.ArtistDisplayName}");
                }
                await _artistListRepo.CreateAsync(ArtistListModel);
                return Ok(ArtistListModel.ToArtistListDto());

            }
            catch (APIException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Response?.StatusCode);
                return StatusCode(500, "Error occurred when processing your request");
            }
            
        }
    }
}
