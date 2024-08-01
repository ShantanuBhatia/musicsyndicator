using MSMS.Server.Models;
using SpotifyAPI.Web;

namespace MSMS.Server.Helpers
{
    public class SpotifyUtils
    {
        public static async Task<List<string>> GetLatestSinglesArtistList(SpotifyClient client, string artistId, int numMonths, ArtistList artistList, bool firstTrackOnly=true)
        {
            DateTime startDate = DateTime.Now.AddMonths(-numMonths);
            List<SimpleAlbum> allSingles = new List<SimpleAlbum>();

            foreach(Artist artist in artistList.Artists)
            {
                // Fetch first page of singles
                var artistAlbums = await client.Artists.GetAlbums(artist.ArtistSpotifyKey, new ArtistsAlbumsRequest
                {
                    IncludeGroupsParam = ArtistsAlbumsRequest.IncludeGroups.Single,
                    Limit = 50,
                    Offset = 0
                });
                await Console.Out.WriteLineAsync($"Fetched the albums for the artist {artistId}");
                allSingles.AddRange(artistAlbums.Items);

                // Paginate through all results
                while (artistAlbums.Next != null)
                {
                    artistAlbums = await client.NextPage(artistAlbums);
                    allSingles.AddRange(artistAlbums.Items);
                }
            }
            
            // allSingles now contains everyone's singles, so now we need to narrow the range down
            // Filter singles released within the specified time frame and sort them
            // TODO optimize the above Single-fetching to hopefully use fewer API calls
            var recentSingles = allSingles
                .Where(album => DateTime.TryParse(album.ReleaseDate, out DateTime releaseDate) &&
                                releaseDate >= startDate)
                .OrderByDescending(album => album.ReleaseDate)
                .ToList();
            await Console.Out.WriteLineAsync("Got here");
            await Console.Out.WriteLineAsync($"Left with {recentSingles.Count} items");
            // Get track IDs for each single
            List<string> trackIds = new List<string>();
            foreach (var single in recentSingles)
            {
                var albumTracks = await client.Albums.GetTracks(single.Id);
                if (firstTrackOnly)
                {
                    if (albumTracks.Items.Any())
                    {
                        trackIds.Add(albumTracks.Items.First().Name);
                    }
                } else
                {
                    trackIds.AddRange(albumTracks.Items.Select(track => track.Name));
                }
                
            }

            foreach(var trackid in trackIds)
            {
                await Console.Out.WriteLineAsync($"Track {trackid}");
            }

            return trackIds;

        }
    }
}
