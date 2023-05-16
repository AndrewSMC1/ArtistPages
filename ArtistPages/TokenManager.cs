//creates the spotify access token for all other API requests
//using a general token since no aspects of this page require user specific information

using System.Text;
using System.Text.Json;
namespace ArtistPages
{
    public class TokenManager
    {

        private string? SPOTIFY_CLIENT_ID = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID");
        //Don't understand how to get environment variables on azure
        //private string? SPOTIFY_CLIENT_ID = "********";
        private string? SPOTIFY_CLIENT_SECRET = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET");
        //private string? SPOTIFY_CLIENT_SECRET = "***********";
        private DateTime expirationTime;
        private string? current_token;


        private async Task<string> generate_new_token()
        {
            //adapted from Spotify documentation 
            if (SPOTIFY_CLIENT_ID == null || SPOTIFY_CLIENT_SECRET == null)
            {
                throw new Exception("Access Client Tokens not found");
            }
            string auth_string = SPOTIFY_CLIENT_ID + ":" + SPOTIFY_CLIENT_SECRET;
            byte[] auth_bytes = Encoding.UTF8.GetBytes(auth_string);

            string spotify_Auth_String = Convert.ToBase64String(auth_bytes); // auth string for token


            static async Task<string> GetSpotifyToken(string spotify_Auth_String)
            {
                string url = "https://accounts.spotify.com/api/token";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", spotify_Auth_String);

                    var form = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"}
                };
                    var requestBody = new FormUrlEncodedContent(form);

                    var response = await client.PostAsync(url, requestBody);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
            }


            string tokenjson = await GetSpotifyToken(spotify_Auth_String);
            string? spotify_token;
            int spotify_token_time = 0;

            JsonDocument json_parsed = JsonDocument.Parse(tokenjson);
            spotify_token = json_parsed.RootElement.GetProperty("access_token").GetString();


            Console.WriteLine("New Token: **********\n");

            spotify_token_time = json_parsed.RootElement.GetProperty("expires_in").GetInt32();
            //Spotify AccessToken expire every 60min Token refreshes at 50min
            expirationTime = DateTime.UtcNow.AddSeconds(spotify_token_time - 600);


            Console.WriteLine("Token Generated at: " + DateTime.UtcNow);
            Console.WriteLine("Token Expires at: " + expirationTime + "\n");


            if (spotify_token == null)
            {
                throw new Exception("Call to Spotify token failed");
            }

            current_token = spotify_token;
            return current_token;
        }

        public async Task<string> get_token() //method that is used to request a token through the project
        {
            if (current_token == null || DateTime.UtcNow >= expirationTime)
            {
                Console.WriteLine("Generating new token");
                await generate_new_token();

            }

            return current_token;

        }

    }
}
