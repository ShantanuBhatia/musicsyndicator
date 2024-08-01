using MSMS.Server.Models;
using MSMS.Server.Data;
using MSMS.Server.Dtos;
using Microsoft.EntityFrameworkCore;
using MSMS.Server.Interfaces;

namespace MSMS.Server.Mappers
{
    public static class ArtistListMappers
    {
        public static async Task<ArtistList> ToArtistListFromCreateDto(this CreateArtistListDto aldto, IArtistRepository artistRepo)
        {
            async Task<List<Artist>> MapArtistIdsToArtistList(List<string> artistIds)
            {
                var artists = new List<Artist>();
                /*
                 For each artist ID we encounter, we check if it exists in the DB already. 
                If yes, add it immediately. If not, we create it to DB and then add it.

                 */
                foreach(var artistId in artistIds)
                {
                    //var thisArtist = await context.Artists.SingleOrDefaultAsync(artist => artist.ArtistSpotifyKey == artistId);
                    var thisArtist = await artistRepo.GetByServiceKeyAsync(artistId);
                    if (thisArtist == null)
                    {
                        Console.WriteLine("Artist " + artistId + " not found. Querying Spotify and adding it to DB...");
                        Console.WriteLine("This line should be replaced by call to Spotify API to provide an actual artist name");
                        Artist newArtist = new Artist
                        {
                            ArtistDisplayName = "TEMP NAME PLZ REMOVE",
                            ArtistSpotifyKey = artistId
                        };
                        // Don't need to manually deal with potential duplicates here
                        // Entity framework saves us from repeats
                        await artistRepo.CreateAsync(newArtist);
                    }


                    Console.WriteLine("Save changes ran, so now turning these all into artist objects to return");
                    artists.Add(thisArtist);
                }
                return artists;
            }

            var artistList = new ArtistList
            {
                ArtistListName = aldto.ArtistListName,
                UserId = aldto.UserId,
                Artists = await MapArtistIdsToArtistList(aldto.ArtistIds)
            };
            return artistList;
        }

        public static ArtistListDto ToArtistListDto(this ArtistList artistList)
        {
            var artistListDto = new ArtistListDto
            {
                ArtistListId = artistList.ArtistListId,
                ArtistListName = artistList.ArtistListName,
                UserId = artistList.UserId,
            };

            var artistDtoList = artistList.Artists.Select(a => a.ToArtistDto()).ToList();
            artistListDto.Artists = artistDtoList;
            return artistListDto;
        }

        
    }
}

