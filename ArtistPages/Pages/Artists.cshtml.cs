using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ArtistPages.Pages
{

    public class ArtistsModel : PageModel
    {
        private ArtistsData ArtistsInfo;
        private readonly ArtistsInfo _artistsInfo;

        public ArtistsModel(ArtistsInfo artistsInfo)
        {
            _artistsInfo = artistsInfo;

        }

        public async Task Pull_ArtistInfo()
        {
            ArtistsInfo = await _artistsInfo.GetArtistInfo();
            
        }
        private List<string> BuildArtistNames(ArtistsData artistsData)
        {
            List<string> artistNames = new List<string>();
           
            foreach (var artist in artistsData.artists)
            {
                artistNames.Add(artist.name);
            }
            return artistNames;
        }
        public List<string> GetArtistNames()
        {
            return BuildArtistNames(ArtistsInfo);
        }

        public string Get_Artist_Name(int index)
        {
            string name = ArtistsInfo.artists[index].name;
            return name;
        }

        public int Get_Artist_Count() {

            int artist_count = ArtistsInfo.artists.Length;
            return artist_count;
        }

        public string Get_Artist_Followers(int index)
        {
            int follower_count = ArtistsInfo.artists[index].followers.total;
            string follower_ammount = Convert.ToString(follower_count);
            return follower_ammount;
        }

        public string Get_Artist_Popularity(int index)
        {
            int popularity = ArtistsInfo.artists[index].popularity;
            string popularity_index = Convert.ToString(popularity);
            return popularity_index;
        }

        public Image Get_Artist_Image(int index)
        {
            Image image = ArtistsInfo.artists[index].images[0];
            return image;
        }

        public async void OnGet()
        {
            
            Console.WriteLine("Artist Page Refreshed");
            //await Pull_ArtistInfo();

            //Console.WriteLine(Get_Artist_Count());

            //Console.WriteLine(Get_Artist_Name(2));
            //Console.WriteLine(Get_Artist_Followers(2));
            //Console.WriteLine(Get_Artist_Popularity(2));
            //Console.WriteLine(Get_Artist_Image(2).url);
            //Console.WriteLine(Get_Artist_Image(2).width);
            //Console.WriteLine(Get_Artist_Image(2).height);
            

        }
    }
}
