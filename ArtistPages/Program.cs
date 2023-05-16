
using ArtistPages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// creates a single instance that will be reused
builder.Services.AddSingleton<TokenManager>();
builder.Services.AddSingleton<ArtistsInfo>();
builder.Services.AddSingleton<ArtistList>();
builder.Services.AddSingleton<ArtistLatestRelease>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// loads the private keys into environment from .env file located in same folder as program.cs
DotNetEnv.Env.Load(".env");

//creates a var that holds the single instance dependency TokenManager
var tokenManager = app.Services.GetRequiredService<TokenManager>();

// Generate the token at startup and stores it in tokenManager for use in other methods
// Note probably not the most secure
await tokenManager.get_token();


// creates the var that holds the Cached instance of artist information
var artistsInfo = app.Services.GetRequiredService<ArtistsInfo>();
// Fetches the json string for all artists at server startup 
await artistsInfo.GetArtistInfo();


// makes the artistList cache stored as part of the program.cs
var artistList = app.Services.GetRequiredService<ArtistList>();
// Creates the list of artists from the artist list class
var artists = artistList.GetArtistList();


// holds the Cached instance of artist Albums
var artistTracks = app.Services.GetRequiredService<ArtistLatestRelease>();
// Fetches the json string for all artists at server startup 
foreach (var artist in artists)//goes through each artist and caches all the latest album information at startup
{
    Console.WriteLine("Startup Albums Requested");
    await artistTracks.GetArtistInfo(artist);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

await app.RunAsync();


