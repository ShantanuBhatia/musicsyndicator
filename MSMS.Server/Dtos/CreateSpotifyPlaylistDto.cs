using System.ComponentModel.DataAnnotations;

namespace MSMS.Server.Dtos
{
    public class CreateSpotifyPlaylistDto
    {
        [Required]
        public int ArtistListId { get; set; }
        public string SpotifyPlaylistName { get; set; } = String.Empty;
    }
}
