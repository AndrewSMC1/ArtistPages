using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtistPages.Pages;

public class IndexModel : PageModel
{
    public string GoogleMapsAPIKey = "";
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}