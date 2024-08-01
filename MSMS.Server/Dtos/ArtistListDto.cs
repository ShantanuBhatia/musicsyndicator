using MSMS.Server.Models;

namespace MSMS.Server.Dtos
{
    public class ArtistListDto
    {
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; } = string.Empty;
        public int UserId { get; set; }
        // TODO figure out how this model should actually reference artists
        public List<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
    }
}
