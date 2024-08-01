using MSMS.Server.Dtos;
using MSMS.Server.Models;

namespace MSMS.Server.Mappers
{
    public static class SpotifyPlaylistMappers
    {
        public static SpotifyPlaylist ToSpotifyPlaylistFromCreateDto(this CreateSpotifyPlaylistDto spdto, string userId, string playlistId)
        {
            return new SpotifyPlaylist
            {
                ArtistListId = spdto.ArtistListId,
                SpotifyPlaylistName = spdto.SpotifyPlaylistName,
                UserId = userId,
                SpotifyPlaylistId = playlistId
            };
        }

        public static PlaylistDto ToPlaylistDto(this SpotifyPlaylist playlist)
        {
            return new PlaylistDto
            {
                SpotifyPlaylistId = playlist.SpotifyPlaylistId,
                SpotifyPlaylistName = playlist.SpotifyPlaylistName,
                UserId = playlist.UserId
            };
        }
    }
}
