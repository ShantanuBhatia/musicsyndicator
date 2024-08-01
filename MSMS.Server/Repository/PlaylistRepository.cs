using Microsoft.EntityFrameworkCore;
using MSMS.Server.Data;
using MSMS.Server.Interfaces;
using MSMS.Server.Models;
using System.ComponentModel;

namespace MSMS.Server.Repository
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ApplicationDBContext _context;

        public PlaylistRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<SpotifyPlaylist> GetPlaylistAsync(string playlistId)
        {
            return await _context.SpotifyLists.FirstOrDefaultAsync(sl => sl.PlaylistKey == playlistId);
        }
        public async Task<List<SpotifyPlaylist>> GetAllPlaylistForUserAsync(string userId)
        {
            return await _context.SpotifyLists.Where(sl => sl.UserId == userId).ToListAsync();
        }
        public async Task<List<SpotifyPlaylist>> GetAllSync()
        {
            return await _context.SpotifyLists.ToListAsync();
        }
        public async Task<SpotifyPlaylist> CreateAsync(SpotifyPlaylist playlist)
        {
            await _context.SpotifyLists.AddAsync(playlist);
            await _context.SaveChangesAsync();
            return playlist;
        }
        public async Task<SpotifyPlaylist> GetByArtistListIdAsync(int artistListId)
        {
            return await _context.SpotifyLists.FirstOrDefaultAsync(sl => sl.ArtistListId == artistListId);
        }
    }
}
