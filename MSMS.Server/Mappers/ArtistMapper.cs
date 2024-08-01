using System.Xml.Linq;
using MSMS.Server.Dtos;
using MSMS.Server.Models;

namespace MSMS.Server.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistDto ToArtistDto(this Artist ArtistModel) {
            return new ArtistDto
            {
                ArtistSpotifyKey = ArtistModel.ArtistSpotifyKey,
                ArtistDisplayName = ArtistModel.ArtistDisplayName,
            };
        }

        public static Artist ToArtistFromDto(this ArtistDto artistDto)
        {
            return new Artist
            {
                ArtistDisplayName = artistDto.ArtistDisplayName,
                ArtistSpotifyKey = artistDto.ArtistSpotifyKey
            };
        }

    }
}
