using Microsoft.AspNetCore.Mvc;
using YouTubeDownloader.Contexts;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.Controllers;

[Route("api/[controller]")]
public class SongController : Controller
{
    private SongsDbContext dbContext;
    
    public SongController(SongsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET
    public string Index()
    {
        return "string";
    }
    // Add a Song to the Database
    [HttpPost]
    [Route("Add")]
    public string AddSong([FromBody] Song song)
    {
        // Add the song to the database
        dbContext.Songs.Add(song);
        dbContext.SaveChanges();
        return "Song Added";
    }
    
    // Add a list of songs to the database
    [HttpPost]
    [Route("AddList")]
    public string AddSongList([FromBody] List<Song> songs)
    {
        // Add the song to the database
        dbContext.Songs.AddRange(songs);
        dbContext.SaveChanges();
        return "Songs Added";
    }
}