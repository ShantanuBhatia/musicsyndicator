namespace MSMS.Server.Dtos
{
    public class PlaylistDto
    {
        public string SpotifyPlaylistId { get; set; }
        public string SpotifyPlaylistName { get; set; } = "defaultname";
        public string UserId { get; set; }
    }
}
