using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPages.Pages
{

    public class ArtistsModel : PageModel
    {
        private ArtistsData ArtistsInfo;
        private readonly ArtistsInfo _artistsInfo;
        private readonly ArtistLatestRelease _artistLatestRelease;

        //pulls in the classes that provide the latest artist and album information
        public ArtistsModel(ArtistsInfo artistsInfo, ArtistLatestRelease artistLatestRelease)
        {
            _artistsInfo = artistsInfo;
            _artistLatestRelease = artistLatestRelease;
        }

        public async Task Pull_ArtistInfo()
        {
            //when the webpage is accessed it ensures that artist information is already received from API before displaying content
            ArtistsInfo = await _artistsInfo.GetArtistInfo();

        }


        // Method that will return artists name at an index
        public string Get_Artist_Name(int index)
        {
            string name = ArtistsInfo.artists[index].name;
            return name;
        }

        // gives the total number of artists who are loaded for the display loop
        public int Get_Artist_Count()
        {
            int artist_count = ArtistsInfo.artists.Length;
            return artist_count;
        }

        //gives the amount of followers 
        public string Get_Artist_Followers(int index)
        {
            int follower_count = ArtistsInfo.artists[index].followers.total;
            string follower_amount = Convert.ToString(follower_count);
            return follower_amount;
        }

        //gives the profile url of an artist
        public string Get_Artist_URL(int index)
        {
            string artistid = ArtistsInfo.artists[index].id;
            string artistURL = $"https://open.spotify.com/artist/{artistid}";
            return artistURL;
        }

        //gives the id of an artist
        public string Get_Artist_ID(int index)
        {
            string artistid = ArtistsInfo.artists[index].id;
            return artistid;
        }

        //gives the profile image url of an artist
        public Image Get_Artist_Image(int index)
        {
            Image image = ArtistsInfo.artists[index].images[0];
            return image;
        }

        //album info is only rerequestable 1 artist at a time each tile needs a new call for album information
        public async Task<string> Get_Album_Name(string artistId)
        {
            ArtistTrack artistTrack = await _artistLatestRelease.GetArtistInfo(artistId);
            string albumName = artistTrack.items[0].name;
            return albumName;

        }

        public async Task<string> Get_Album_Img(string artistId)
        {
            ArtistTrack artistTrack = await _artistLatestRelease.GetArtistInfo(artistId);

            string albumImg = artistTrack.items[0].images[0].url;
            return albumImg;
        }

        public async Task<string> Get_Album_Date(string artistId)
        {
            ArtistTrack artistTrack = await _artistLatestRelease.GetArtistInfo(artistId);

            string albumDate = artistTrack.items[0].release_date;
            return albumDate;
        }

        public async Task<string> Get_Album_URL(string artistId)
        {
            ArtistTrack artistTrack = await _artistLatestRelease.GetArtistInfo(artistId);

            string albumUrl = artistTrack.items[0].external_urls.spotify;
            return albumUrl;
        }

        public void OnGet()
        {

            Console.WriteLine("Artist Page Refreshed");

        }
    }
}
