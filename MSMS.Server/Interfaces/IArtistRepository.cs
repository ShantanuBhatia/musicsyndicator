using MSMS.Server.Models;

namespace MSMS.Server.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllKnownArtistsAsync();
        Task<Artist?> GetByServiceKeyAsync(string serviceKey);
        Task<Artist> CreateAsync(Artist artist);
        Task<List<Artist>> SearchServiceByNameAsync(string name);
    }
}
