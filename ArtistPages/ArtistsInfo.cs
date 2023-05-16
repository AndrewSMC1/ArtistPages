using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static ArtistPages.ArtistsInfo;

namespace ArtistPages
{
    public class ArtistsInfo
    {
        private ArtistsData artistCacheData;
        
        private DateTime refreshTime;

        private List<string> artists = new List<string>();
        // creates the list of artists can be done in db or json for better organization
        private void Appendartists()
        {
            artists.Clear();
            Console.WriteLine("Regenerating Artists List");
            artists.Add("0k17h0D3J5VfsdmQ1iZtE9");
            artists.Add("0Z8XVUAOBPM4x12wKnFHEQ");
            artists.Add("4RZjexLlwgAPorkk7AUKT7");

        }
        // puts all artists in a string seperated by commas
        private string LinkArtists()
        {
            Appendartists();
            Console.WriteLine("Linking Artists Together\n");
            string combinedArtists = string.Join(",", artists);
            return combinedArtists;
        }
        
        // gets a valid token for calls to the Spotify API from TokenManager class
        private readonly TokenManager _tokenManager;
        public ArtistsInfo(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;

        }

        // Makes a request to the Spofity API for general artists info returns json
        private static async Task<string> GetArtistInfoAsync(string artistIds, string accessToken)
        {
            string url = $"https://api.spotify.com/v1/artists?ids={artistIds}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        // called to pull artist info either gets new or pulls from stored objects
        private async Task<ArtistsData> GenerateArtistInfo()
        {
            Console.Out.WriteLine("Receving Artists Data\n");
            string accessToken = await _tokenManager.get_token();
            string artistInfoJson = await GetArtistInfoAsync(LinkArtists(), accessToken);
            
            
            return JsonSerializer.Deserialize<ArtistsData>(artistInfoJson);

        }


        public async Task<ArtistsData> GetArtistInfo()
        {

            if (artistCacheData == null || DateTime.UtcNow >= refreshTime) // Check if the data is already retrieved
            {
                await Console.Out.WriteLineAsync("No Artist Data Found Calling Spotify API");
                //artistCacheData = null;
                artistCacheData = await GenerateArtistInfo();
                // Calls API to refresh Artist Data every 15 mins reduces API calls
                refreshTime = DateTime.UtcNow.AddSeconds(30);
                await Console.Out.WriteLineAsync("Recived Data at: "+ DateTime.UtcNow);
                await Console.Out.WriteLineAsync("Next Refresh at: "+ refreshTime + "\n");
                return artistCacheData;
               
            }
            else { 
                // TODO make returned value readonly
                return artistCacheData;
            }

        }

    }


    public class ArtistsData
    {
        public Artist[] artists { get; set; }
    }

    public class Artist
    {
        public External_Urls external_urls { get; set; }
        public Followers followers { get; set; }
        public string[] genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class External_Urls
    {
        public string spotify { get; set; }
    }

    public class Followers
    {
        public string href { get; set; }
        public int total { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

   

}
