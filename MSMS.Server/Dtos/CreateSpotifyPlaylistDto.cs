namespace MSMS.Server.Dtos
{
    public class CreateSpotifyPlaylistDto
    {
        public string SpotifyPlaylistName { get; set; } = "defaultname";
        public int ArtistListId { get; set; }
    }
}
