namespace MSMS.Server.Models
{
    public class ArtistList
    {
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; }
        public int UserId { get; set; }
        // TODO figure out how this model should actually reference artists
        public List<int> Artists { get; set; } = []; 
    }
}
