// class that requests information for all artists profile data
using System.Text.Json;

namespace ArtistPages
{
    public class ArtistsInfo
    {
        private ArtistsData artistCacheData;

        private DateTime refreshTime;
        ArtistList artistList = new ArtistList();




        // gets a valid token for calls to the Spotify API from TokenManager class
        private readonly TokenManager _tokenManager;
        public ArtistsInfo(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        // Makes a request to the Spotify API for general artists info returns json
        private static async Task<string> GetArtistInfoAsync(string artistIds, string accessToken)
        {
            string url = $"https://api.spotify.com/v1/artists?ids={artistIds}";

            using (var client = new HttpClient()) //opens connection while in use
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }

        }

        private async Task<ArtistsData> GenerateArtistInfo()
        {
            Console.Out.WriteLine("Receiving Artists Data\n");
            string accessToken = await _tokenManager.get_token();
            string artistInfoJson = await GetArtistInfoAsync(artistList.LinkArtists(), accessToken);
            return JsonSerializer.Deserialize<ArtistsData>(artistInfoJson);

        }

        // called to pull artist info either gets new or pulls from stored objects
        public async Task<ArtistsData> GetArtistInfo()
        {
            if (artistCacheData == null || DateTime.UtcNow >= refreshTime) // Check if the data is already retrieved
            {
                await Console.Out.WriteLineAsync("Artist Data Expired Refreshing");
                //artistCacheData = null;
                artistCacheData = await GenerateArtistInfo();
                // Calls API to refresh Artist Data every 15 mins reduces API calls
                refreshTime = DateTime.UtcNow.AddSeconds(900);
                await Console.Out.WriteLineAsync("Received Data at: " + DateTime.UtcNow);
                await Console.Out.WriteLineAsync("Next Refresh at: " + refreshTime + "\n");

            }
            return artistCacheData; //returns valid artist profile data
        }

    }

}





