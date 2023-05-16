namespace ArtistPages
{

    public class ArtistsTrackData
    {
        public ArtistTrack[]? artistTracks { get; set; }
    }
    public class ArtistTrack
    {
        public string href { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string album_type { get; set; }
        public int total_tracks { get; set; }
        public string[] available_markets { get; set; }
        public External_Urls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public Restrictions restrictions { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public Copyright[] copyrights { get; set; }
        public External_Ids external_ids { get; set; }
        public string[] genres { get; set; }
        public string label { get; set; }
        public int popularity { get; set; }
        public string album_group { get; set; }
        public Artist[] artists { get; set; }
    }

    public class Album_External_Urls
    {
        public string spotify { get; set; }
    }

    public class Restrictions
    {
        public string reason { get; set; }
    }

    public class External_Ids
    {
        public string isrc { get; set; }
        public string ean { get; set; }
        public string upc { get; set; }
    }

    public class Album_Image
    {
        public string url { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Copyright
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Album_Artist
    {
        public External_Urls1 external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class External_Urls1
    {
        public string spotify { get; set; }
    }

}
