using System.ComponentModel.DataAnnotations;

namespace MSMS.Server.Models
{
    public class ArtistList
    {
        [StringLength(255)]
        public string UserId { get; set; } = string.Empty;
        public int ArtistListId { get; set; }
        public string ArtistListName { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        // TODO figure out how this model should actually reference artists
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public SpotifyPlaylist? LinkedPlaylist { get; set; }
    }
}
