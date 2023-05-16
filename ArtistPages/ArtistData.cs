//class that holds the information returned by the artist information API request
namespace ArtistPages
{
    public class ArtistsData
    {
        public Artist[]? artists { get; set; }
    }

    public class Artist
    {
        public External_Urls? external_urls { get; set; }
        public Followers? followers { get; set; }
        public string[]? genres { get; set; }
        public string? href { get; set; }
        public string? id { get; set; }
        public Image[]? images { get; set; }
        public string? name { get; set; }
        public int popularity { get; set; }
        public string? type { get; set; }
        public string? uri { get; set; }
    }

    public class External_Urls
    {
        public string? spotify { get; set; }
    }

    public class Followers
    {
        public string? href { get; set; }
        public int total { get; set; }
    }

    public class Image
    {
        public string? url { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }
}
