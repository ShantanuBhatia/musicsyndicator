namespace MSMS.Server.Models
{
    public class CreateArtistListDto
    {
        public int UserId { get; set; }
        public List<int> ArtistIds { get; set; } = [];
    }
}
