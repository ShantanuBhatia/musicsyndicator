namespace MSMS.Server.Models
{
    public class CreateArtistListDto
    {
        public List<string>? ArtistIds { get; set; } = [];
        public string ArtistListName { get; set; }
    }
}
