namespace MSMS.Server.Models
{
    public class ArtistList
    {
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; }
        public int UserId { get; set; }
        public List<Artist> Artists { get; set; } = [];
    }
}
