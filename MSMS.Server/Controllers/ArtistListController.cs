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
            await Console.Out.WriteLineAsync("############HERE IS MY SPOTIFY ID: ");
            await Console.Out.WriteLineAsync(spotifyUserId);

            if (string.IsNullOrEmpty(spotifyUserId))
            {
                return Unauthorized("User ID not found in the authentication token.");
            }
            // Call the repository method to get all lists for the user
            var artistLists = await _artistListRepo.GetAllForUserAsync(9);

            // Return the list of artist lists
            return Ok(artistLists);
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
            var ArtistListModel = await ListCreationData.ToArtistListFromCreateDto(_artistRepo);
            Console.WriteLine("Writing the artist list goes here");
            foreach(var a in ArtistListModel.Artists)
            {
                Console.WriteLine(a);
            }
            await _artistListRepo.CreateAsync(ArtistListModel);
            return Ok(ArtistListModel);
        }
    }
}
