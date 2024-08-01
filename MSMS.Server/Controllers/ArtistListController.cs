using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Mappers;
using MSMS.Server.Models;

using MSMS.Server.Data;
using Microsoft.EntityFrameworkCore;
using MSMS.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SpotifyAPI.Web;
using MSMS.Server.Dtos;
using MSMS.Server.Helpers;

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
            await Console.Out.WriteLineAsync("Hot reload you absolute pile of Microsoft");
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

                var ArtistListModel = await ListCreationData.ToArtistListFromCreateDto(_artistRepo, spotifyUserId, spotify);
                Console.WriteLine("Writing the artist list goes here");
                foreach (var a in ArtistListModel.Artists)
                {
                    Console.WriteLine($"Spotify key: {a.ArtistSpotifyKey}, artist name {a.ArtistDisplayName}");
                }
                await _artistListRepo.CreateAsync(ArtistListModel);
                await Console.Out.WriteLineAsync("Latest version of code aaaa");
                return Ok(ArtistListModel.ToArtistListDto());

            }
            catch (APIException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Response?.StatusCode);
                return StatusCode(500, "Error occurred when processing your request");
            }
            
        }

        [HttpPost("createplaylist")]
        public async Task<IActionResult> CreatePlaylistFromArtistList([FromBody]CreateSpotifyPlaylistDto playlistCreationDto)
        {
            var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var artistList = await _artistListRepo.GetByIDAsync(playlistCreationDto.ArtistListId);

            await Console.Out.WriteLineAsync($"Fetched the artistlist called {artistList.ArtistListName}");
            // I know that this error message is super vague, maybe even inaccurate
            // But this particular code path can only come from someone tryna access other users' data
            // In which case it would be standard practice to be as vague as possible
            // And not give anything away
            // TODO - protect against this malarkey by giving ArtistLists a non-sequential ID
            if (artistList is null || spotifyUserId != artistList.UserId)
            {
                return BadRequest("An error occurred.");
            }

            if (string.IsNullOrEmpty(spotifyUserId))
            {
                return Unauthorized("User ID not found in the authentication token.");
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var spotify = new SpotifyClient(accessToken);

            var tracklist = await SpotifyUtils.GetLatestSinglesArtistList(spotify, artistList.Artists[0].ArtistSpotifyKey, 12, artistList);

            // Now that we have a track list, let's try to create the actual playlist
            try
            {
                var playlistCreationRequest = new PlaylistCreateRequest(playlistCreationDto.SpotifyPlaylistName)
                {
                    Public = false,
                    Description = "Maintained by the Moderately Simple Music Syndication service"
                };

                var playlist = await spotify.Playlists.Create(spotifyUserId, playlistCreationRequest);
                
                // Add tracks to the playlist
                // Spotify API limits adding tracks to 100 per request, so we need to batch them
                const int batchSize = 100;
                for (int i = 0; i < tracklist.Count; i += batchSize)
                {
                    var batch = tracklist.GetRange(i, Math.Min(batchSize, tracklist.Count - i));
                    await spotify.Playlists.AddItems(playlist.Id, new PlaylistAddItemsRequest(batch));
                }

                return Ok(playlist.Id);
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return BadRequest(new { Error = ex.Message });
            }

            
        }
    }
}
