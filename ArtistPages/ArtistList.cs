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
            //Soviet Soviet
            artists.Add("5BC3lvcEEOqVrqMaPjYrgu");
            artists.Add("6olE6TJLqED3rqDCT0FyPh");
            artists.Add("5qRtt9DQNy64ig66kCWFjX");

            //RadioHead
            artists.Add("4Z8W4fKeB5YxbusRsdQVPb");
            //Baby Keem
            artists.Add("5SXuuuRpukkTvsLuUknva1");
            //Gorillaz
            artists.Add("3AA28KZvwAUcZuOKwyblJQ");
            //Mac Miller
            artists.Add("4LLpKhyESsyAXpc4laK94U");
            //Travis Scott
            artists.Add("0Y5tJX1MQlPlqiwlOH1tJY");
            //Flavors
            artists.Add("6Amqc7UjJa19q4jrfAHA77");
            //Beach House
            artists.Add("56ZTgzPBDge0OvCGgMO3OY");
            //The Strokes
            artists.Add("0epOFNiUfyON9EYx7Tpr6V");
            //Frownin
            artists.Add("4RZjexLlwgAPorkk7AUKT7");
            //Scott James
            artists.Add("2FTAdT6BZIUQCRwIwdA9Dy");
            //The Voidz
            artists.Add("4nUBBtLtzqZGpdiynTJbYJ");
            //Frank Ocean
            artists.Add("2h93pZq0e7k5yf4dywlkpM");
            
       

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
