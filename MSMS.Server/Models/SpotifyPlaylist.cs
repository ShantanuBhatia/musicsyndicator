namespace MSMS.Server.Models
{
    public class SpotifyPlaylist
    {
        public string SpotifyPlaylistId { get; set; }
        public string SpotifyPlaylistName { get; set; } = "defaultname";
        public int UserId { get; set;}
        public int ArtistListId { get; set; }
    }
}
