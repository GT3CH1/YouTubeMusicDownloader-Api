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
        // Check to see if the song already exists
        if (dbContext.Songs.Any(s => s.Url == song.Url))
            return "Song already exists";
        else
        {
            dbContext.Songs.Add(song);
            dbContext.SaveChanges();
            return "Song added";
        }
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
    
    // Get a list of all songs in the database
    [HttpGet]
    [Route("GetAll")]
    public JsonResult GetAllSongs()
    {
        return Json(dbContext.Songs.ToList());
    }
    
}