namespace MSMS.Server.Models
{
    public class ArtistList
    {
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        // TODO figure out how this model should actually reference artists
        public List<Artist> Artists { get; set; } = new List<Artist>(); 
    }
}
