using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPages.Pages;

public class IndexModel : PageModel
{
    public string GoogleMapsAPIKey;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        string MAPKEY = "";
        MAPKEY += Environment.GetEnvironmentVariable("MAPKEY");
        if (MAPKEY == "")
        {
            throw new Exception("MAPKEY environment variable is not set.");
        }

        GoogleMapsAPIKey = $"https://maps.googleapis.com/maps/api/js?key={MAPKEY}&callback=initMAP";

    }
}