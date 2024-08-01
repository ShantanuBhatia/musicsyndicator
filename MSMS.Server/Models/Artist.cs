using Microsoft.Identity.Client;

namespace MSMS.Server.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string ArtistSpotifyKey { get; set; } = string.Empty;
        public string ArtistDisplayName { get; set; } = string.Empty;
        // Navigation
        public List<ArtistList>? ArtistLists { get; }
    }
}
