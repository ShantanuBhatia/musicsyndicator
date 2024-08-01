using MSMS.Server.Models;

namespace MSMS.Server.Mappers
{
    public static class ArtistListMappers
    {
        public static ArtistList ToArtistListFromCreateDto(this CreateArtistListDto aldto)
        {
            return new ArtistList
            {
                ArtistListName = aldto.ArtistListName,
                Artists = aldto.ArtistIds,
                UserId = aldto.UserId,
            };
        }
    }
}
