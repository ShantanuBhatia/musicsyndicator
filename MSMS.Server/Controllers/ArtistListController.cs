using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Mappers;
using MSMS.Server.Models;

using MSMS.Server.Data;
using Microsoft.EntityFrameworkCore;
using MSMS.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/artistlists")]
    [Authorize]
    public class ArtistListController : ControllerBase
    {
        private static readonly string[] TempArtists = ["Stray Kids", "RESCENE", "NewJeans", "LeSserafim"];

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
            // Call the repository method to get all lists for the user
            var artistLists = await _artistListRepo.GetAllForUserAsync(spotifyUserId);
            var allArtistListDtos = artistLists.Select(al => al.ToArtistListDto());
            // Return the list of artist lists
            return Ok(allArtistListDtos);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArtistListById([FromRoute] int id)
        {
            //var artistList = await _context.ArtistLists.FindAsync(id);
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
            var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(spotifyUserId))
            {
                return Unauthorized("User ID not found in the authentication token.");
            }

            // DONE refactor creatartistlistdto to remove the user ID from the object body
            // DONE modify the method call below to take two params - artist repo, and the user ID
            // From there on out, the logic is identical
            // let's try it!!!



            var ArtistListModel = await ListCreationData.ToArtistListFromCreateDto(_artistRepo, spotifyUserId);
            Console.WriteLine("Writing the artist list goes here");
            foreach(var a in ArtistListModel.Artists)
            {
                Console.WriteLine($"Spotify key: {a.ArtistSpotifyKey}, artist name {a.ArtistDisplayName}");
            }
            await _artistListRepo.CreateAsync(ArtistListModel);
            return Ok(ArtistListModel);
        }
    }
}
