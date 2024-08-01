namespace MSMS.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string SpotifyId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; }
    }
}
