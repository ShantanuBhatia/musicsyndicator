﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using MSMS.Server.Helpers;
using System.Security.Claims;

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
        [Authorize(Policy = "SpotifyNoRedirect")]
        public async Task<IActionResult> SearchArtists(string query)
        {
            
            try
            {
                var spotify = await _spotifyClientBuilder.BuildClient();
                var spotifyUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(spotifyUserId))
                {
                    return Unauthorized("Could not authenticate user.");
                }


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
