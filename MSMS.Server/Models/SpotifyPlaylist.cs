namespace MSMS.Server.Models
{
    public class SpotifyPlaylist
    {
        public string PlaylistKey {  get; set; }
        public string SpotifyPlaylistId { get; set; }
        public string SpotifyPlaylistName { get; set; } = "defaultname";
        public string UserId { get; set;}
        public int ArtistListId { get; set; }
        public ArtistList LinkedArtistList { get; set; }
    }
}
