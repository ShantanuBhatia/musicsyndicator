using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSMS.Server.Dtos;
using MSMS.Server.Helpers;
using MSMS.Server.Interfaces;
using MSMS.Server.Mappers;
using MSMS.Server.Models;
using SpotifyAPI.Web;
using System.Security.Claims;

namespace MSMS.Server.Controllers
{
    [ApiController]
    [Route("api/playlists")]
    [Authorize]
    public class PlaylistController : ControllerBase
    {

        private readonly IPlaylistRepository _playlistRepo;
        private readonly IArtistListRepository _artistListRepo;

        public PlaylistController(IPlaylistRepository playlistRepository, IArtistListRepository artistListRepository)
        {

            _playlistRepo = playlistRepository;
            _artistListRepo = artistListRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById([FromRoute] string id)
        {
            var playlist = await _playlistRepo.GetPlaylistAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist.ToPlaylistDto());
        }
        [HttpPost("createplaylist")]
        public async Task<IActionResult> CreatePlaylistFromArtistList([FromBody] CreateSpotifyPlaylistDto playlistCreationDto)
        {
            var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var artistList = await _artistListRepo.GetByIDAsync(playlistCreationDto.ArtistListId);

            /*
               I know that this error message is super vague, maybe even inaccurate
               But this particular code path can only come from someone tryna access other users' data
               In which case it would be standard practice to be as vague as possible
               And not give anything away
               TODO - protect against this malarkey by giving ArtistLists a non-sequential ID
            */
            if (artistList is null || spotifyUserId != artistList.UserId)
            {
                return BadRequest("An error occurred.");
            }

            if (string.IsNullOrEmpty(spotifyUserId))
            {
                return Unauthorized("User ID not found in the authentication token.");
            }

            // TODO instead of returning error, refresh the playlist
            if (await _playlistRepo.GetByArtistListIdAsync(artistList.ArtistListId) != null)
            {
                return BadRequest("Cannot create duplicate playlist for this artist list");
            }


            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var spotify = new SpotifyClient(accessToken);

            // Get all relevant tracks for 
            var tracklist = await SpotifyUtils.GetLatestSinglesArtistList(spotify, artistList.Artists[0].ArtistSpotifyKey, 12, artistList);


            try
            {
                // TODO pull this description out to some config file?
                var playlistCreationRequest = new PlaylistCreateRequest(playlistCreationDto.SpotifyPlaylistName)
                {
                    Public = false,
                    Description = "Maintained by the Moderately Simple Music Syndication service"
                };

                var playlist = await spotify.Playlists.Create(spotifyUserId, playlistCreationRequest);

                if (playlist is null)
                {
                    return BadRequest("An error occurred initializing the playlist");
                }

                // Add tracks to the playlist
                // Spotify API limits adding tracks to 100 per request, so we need to batch them
                const int batchSize = 100;
                for (int i = 0; i < tracklist.Count; i += batchSize)
                {
                    var batch = tracklist.GetRange(i, Math.Min(batchSize, tracklist.Count - i));
                    await spotify.Playlists.AddItems(playlist.Id, new PlaylistAddItemsRequest(batch));
                }

                SpotifyPlaylist playlistToInsert = playlistCreationDto.ToSpotifyPlaylistFromCreateDto(spotifyUserId, playlist.Id);
                playlistToInsert.PlaylistKey = "temp"; // todo refactor away this useless key

                await _playlistRepo.CreateAsync(playlistToInsert);
                return Ok(playlist.Id);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return BadRequest(new { Error = ex.Message });
            }


        }



    }
}
