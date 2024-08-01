using MSMS.Server.Models;

namespace MSMS.Server.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<SpotifyPlaylist> GetPlaylistAsync(string playlistId);
        Task<List<SpotifyPlaylist>> GetAllPlaylistForUserAsync(string userId);
        Task<List<SpotifyPlaylist>> GetAllSync();
        Task<SpotifyPlaylist> CreateAsync(SpotifyPlaylist playlist);
        Task<SpotifyPlaylist> GetByArtistListIdAsync(int artistListId);
    }
}
