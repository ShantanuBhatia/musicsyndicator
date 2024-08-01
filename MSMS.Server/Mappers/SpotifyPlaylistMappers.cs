using MSMS.Server.Dtos;
using MSMS.Server.Models;

namespace MSMS.Server.Mappers
{
    public static class SpotifyPlaylistMappers
    {
        public static SpotifyPlaylist ToSpotifyPlaylistFromCreateDto(this CreateSpotifyPlaylistDto spdto, string userId)
        {
            return new SpotifyPlaylist
            {
                ArtistListId = spdto.ArtistListId,
                SpotifyPlaylistName = spdto.SpotifyPlaylistName,
                UserId = userId
            };
        }
    }
}
