namespace MSMS.Server.Models
{
    public class CreateArtistListDto
    {
        public int UserId { get; set; }
        public string[] artists { get; set; }
    }
}
