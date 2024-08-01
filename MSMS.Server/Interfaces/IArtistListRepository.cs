using MSMS.Server.Models;

namespace MSMS.Server.Interfaces
{
    public interface IArtistListRepository
    {
        Task<List<ArtistList>> GetAllAsync();
        Task<ArtistList?> GetByIDAsync(int id);
        Task<ArtistList> CreateAsync(ArtistList artistList);
    }
}
