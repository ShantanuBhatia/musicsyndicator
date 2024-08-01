using MSMS.Server.Data;
using MSMS.Server.Interfaces;
using MSMS.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MSMS.Server.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDBContext _context;
        public ArtistRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Artist>> GetAllKnownArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }
        public async Task<Artist?> GetByServiceKeyAsync(string serviceKey)
        {
            return await _context.Artists.SingleOrDefaultAsync(artist => artist.ArtistSpotifyKey == serviceKey);
        }
        public async Task<Artist> CreateAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;
        }
        public async Task<List<Artist>> SearchServiceByNameAsync(string queryName)
        {
            //return await _context.Artists.Where(artist => artist.ArtistDisplayName.ToLower().Contains(name.ToLower())).ToListAsync();
            var artists = _context.Artists.AsQueryable();
            artists = artists.Where(artist => artist.ArtistDisplayName.ToLower().Contains(queryName.ToLower()));
            return await artists.ToListAsync();
            
        }
    }
}
