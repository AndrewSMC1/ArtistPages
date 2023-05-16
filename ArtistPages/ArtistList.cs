namespace ArtistPages
{
    public class ArtistList
    {
        public List<string> artists = new List<string>();
        // creates the list of artists can be done in db or json for better organization
        private void GenerateArtistList()
        {
            artists.Clear();
            Console.WriteLine("Regenerating Artists List");
            artists.Add("0k17h0D3J5VfsdmQ1iZtE9");
            artists.Add("0Z8XVUAOBPM4x12wKnFHEQ");
            artists.Add("4RZjexLlwgAPorkk7AUKT7");
            artists.Add("6olE6TJLqED3rqDCT0FyPh");
            artists.Add("5qRtt9DQNy64ig66kCWFjX");


        }
        // puts all artists in a string separated by commas
        public string LinkArtists()
        {
            GenerateArtistList();
            Console.WriteLine("Linking Artists Together\n");
            string combinedArtists = string.Join(",", artists);
            return combinedArtists;
        }

        public List<string> GetArtistList()
        {
            GenerateArtistList();
            return artists;
        }
    }
}
