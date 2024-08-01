using Microsoft.EntityFrameworkCore;
using MSMS.Server.Data;
using MSMS.Server.Interfaces;
using MSMS.Server.Models;

namespace MSMS.Server.Repository
{
    public class ArtistListRepository : IArtistListRepository
    {
        private readonly ApplicationDBContext _context;

        public ArtistListRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ArtistList>> GetAllAsync()
        {
            return await _context.ArtistLists.Include(a => a.Artists).ToListAsync();
        }

        public async Task<List<ArtistList>> GetAllForUserAsync(string id)
        {
            return await _context.ArtistLists.Include(a => a.Artists).Where(a => a.UserId == id).ToListAsync();
        }
        public async Task<ArtistList?> GetByIDAsync(int id)
        {
            return await _context.ArtistLists.Include(a => a.Artists).FirstOrDefaultAsync(a => a.ArtistListId == id);

        }
        public async Task<ArtistList> CreateAsync(ArtistList artistList)
        {

            await _context.ArtistLists.AddAsync(artistList);
            await _context.SaveChangesAsync();
            return artistList;
        }
        
    }
}
