using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AspNet.Security.OAuth.Spotify;
using Microsoft.EntityFrameworkCore;

using MSMS.Server.Data;
using MSMS.Server.Interfaces;
using MSMS.Server.Repository;
using MSMS.Server.Helpers;
using SpotifyAPI.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// secrets for api keys etc
builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>();


// Database context stuff
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IArtistListRepository, ArtistListRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(SpotifyClientConfig.CreateDefault());
builder.Services.AddScoped<SpotifyClientBuilder>();


// spotify auth stuff 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = SpotifyAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(65);
})
.AddSpotify(options =>
{
    options.ClientId = builder.Configuration["Spotify:ClientId"];
    options.ClientSecret = builder.Configuration["Spotify:ClientSecret"];
    options.CallbackPath = "/signin-spotify-auth";
    options.SaveTokens = true;
    options.Scope.Add("user-read-private");
    options.Scope.Add("user-read-email");
    options.Scope.Add("playlist-modify-public");
    options.Scope.Add("playlist-modify-private"); // I wonder why these are two different permissions?
});

// CORS for react app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});



var app = builder.Build();

// Use CORS
app.UseCors("AllowSpecificOrigin");

app.UseDefaultFiles();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
