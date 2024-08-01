using MSMS.Server.Models;
using MSMS.Server.Data;
using MSMS.Server.Dtos;
using Microsoft.EntityFrameworkCore;
using MSMS.Server.Interfaces;
using SpotifyAPI.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MSMS.Server.Mappers
{
    public static class ArtistListMappers
    {
        public static async Task<ArtistList> ToArtistListFromCreateDto(this CreateArtistListDto aldto, IArtistRepository artistRepo, string userSpotifyID, SpotifyClient client)
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
                    await Console.Out.WriteLineAsync("Hot reloaded?");
                    if (thisArtist == null)
                    {
                        Console.WriteLine("AAAArtist " + artistId + " not found. Querying Spotify and adding it to DB...");
                        //Console.WriteLine("This line should be replaced by call to Spotify API to provide an actual artist name"); <- it should, and now it is :)

                        try
                        {
                            var searchResponse = await client.Artists.Get(artistId);

                            if (searchResponse == null)
                            {
                                await Console.Out.WriteLineAsync($"Couldn't find artist {artistId}. Skipping for now.");
                                // TODO - instead of skipping artist, return error
                                continue;
                            }
                            Artist newArtist = new Artist
                            {
                                ArtistDisplayName = searchResponse.Name,
                                ArtistSpotifyKey = searchResponse.Id
                            };
                            // Don't need to manually deal with potential duplicates here
                            // Entity framework saves us from repeats
                            await artistRepo.CreateAsync(newArtist);
                            thisArtist = newArtist;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("I frew up call mom and keep trucking");
                            Console.WriteLine(ex.Message);
                        }
                    }


                    Console.WriteLine("Save changes ran, so now turning these all into artist objects to return");
                    // TODO refactor this mess so that we only attempt to write good data
                    // nulls should never get here 
                    if (thisArtist != null)
                    {
                        artists.Add(thisArtist);
                    }
                }
                return artists;
            }

            var artistList = new ArtistList
            {
                ArtistListName = aldto.ArtistListName,
                UserId = userSpotifyID,
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

