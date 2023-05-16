using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPages.Pages
{

    public class ArtistsModel : PageModel
    {
        private ArtistsData ArtistsInfo;
        private readonly ArtistsInfo _artistsInfo;

        private readonly ArtistLatestRelease _artistLatestRelease;


        public ArtistsModel(ArtistsInfo artistsInfo, ArtistLatestRelease artistLatestRelease)
        {
            _artistsInfo = artistsInfo;
            _artistLatestRelease = artistLatestRelease;
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

        public int Get_Artist_Count()
        {

            int artist_count = ArtistsInfo.artists.Length;
            return artist_count;
        }

        public string Get_Artist_Followers(int index)
        {
            int follower_count = ArtistsInfo.artists[index].followers.total;
            string follower_amount = Convert.ToString(follower_count);
            return follower_amount;
        }

        public string Get_Artist_URL(int index)
        {
            string artistid = ArtistsInfo.artists[index].id;
            string artistURL = $"https://open.spotify.com/artist/{artistid}";
            return artistURL;
        }

        public string Get_Artist_ID(int index)
        {
            string artistid = ArtistsInfo.artists[index].id;
            return artistid;
        }
        public Image Get_Artist_Image(int index)
        {
            Image image = ArtistsInfo.artists[index].images[0];
            return image;
        }


        public async Task<string> Get_Album_Name(string artistId)
        {
            ArtistTrack artistTrack = await _artistLatestRelease.GetArtistInfo(artistId);
            if (artistTrack != null && artistTrack.items.Length > 0)
            {
                string albumName = artistTrack.items[0].name;
                return albumName;
            }

            return string.Empty; // or throw an exception if no album found
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
            //string albumUrl = $"https://open.spotify.com/track/{albumUri}";
            return albumUrl;
        }




        public void OnGet()
        {

            Console.WriteLine("Artist Page Refreshed");

        }
    }
}
