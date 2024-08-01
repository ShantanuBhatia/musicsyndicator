namespace MSMS.Server.Models
{
    public class CreateArtistListDto
    {
        public int UserId { get; set; }
        public List<string>? ArtistIds { get; set; } = [];
        public string ArtistListName { get; set; }
    }
}
