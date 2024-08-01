namespace MSMS.Server.Models
{
    public class SpotifyPlaylist
    {
        public int SpotifyPlaylistId { get; set; }
        public string SpotifyPlaylistName { get; set; } = "defaultname";
        public int UserId { get; set;}
        public int ArtistListId { get; set; }
    }
}
