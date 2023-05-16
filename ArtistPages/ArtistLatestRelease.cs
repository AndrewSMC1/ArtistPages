using System.Text.Json;

namespace ArtistPages
{
    public class ArtistLatestRelease
    {
        private Dictionary<string, ArtistTrack> artistTrackCache;
        private DateTime refreshTime;
        private ArtistList artistList = new ArtistList();

        private readonly TokenManager _tokenManager;

        public ArtistLatestRelease(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
            artistTrackCache = new Dictionary<string, ArtistTrack>();
        }

        private static async Task<string> GetArtistInfoAsync(string artistId, string accessToken)
        {
            string url = $"https://api.spotify.com/v1/artists/{artistId}/albums";

            using (var client = new HttpClient())
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

        private async Task<ArtistTrack> GenerateArtistInfo(string artistId)
        {
            Console.Out.WriteLine($"Receiving Artist Data for ID: {artistId}\n");
            string accessToken = await _tokenManager.get_token();
            string artistInfoJson = await GetArtistInfoAsync(artistId, accessToken);
            return JsonSerializer.Deserialize<ArtistTrack>(artistInfoJson);
        }

        public async Task<ArtistTrack> GetArtistInfo(string artistId)
        {
            if (!artistTrackCache.ContainsKey(artistId) || DateTime.UtcNow >= refreshTime)
            {
                await Console.Out.WriteLineAsync("Album Data Expired Refreshing");
                artistTrackCache[artistId] = await GenerateArtistInfo(artistId);
                refreshTime = DateTime.UtcNow.AddSeconds(900);
                await Console.Out.WriteLineAsync("Received Data at: " + DateTime.UtcNow);
                await Console.Out.WriteLineAsync("Next Refresh at: " + refreshTime + "\n");
            }
            
            return artistTrackCache[artistId];
        }
    }
}
