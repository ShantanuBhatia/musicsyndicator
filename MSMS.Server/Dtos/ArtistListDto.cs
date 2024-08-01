using MSMS.Server.Models;

namespace MSMS.Server.Dtos
{
    public class ArtistListDto
    {
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string playlistId { get; set; } = string.Empty;
        public List<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
    }
}
