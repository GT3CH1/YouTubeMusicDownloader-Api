using Microsoft.AspNetCore.Mvc;
using YouTubeDownloader.Contexts;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.Controllers;

public class SongController : Controller
{
    private readonly IConfiguration config;
    private readonly SongsDbContext dbContext;

    public SongController(SongsDbContext dbContext, IConfiguration config)
    {
        this.dbContext = dbContext;
        this.config = config;
    }

    // GET
    public IActionResult Index()
    {
        return View(dbContext.Songs.ToList());
    }
    
    public IActionResult Edit(int id)
    {
        Song song = dbContext.Songs.Find(id);
        return View(song);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    // Add a Song to the Database
    [HttpPost]
    public IActionResult Add(Song song)
    {
        // remove id from model state
        ModelState.Remove("Id");

        // check if model state is valid.
        if (ModelState.IsValid)
        {
            // add song to database
            dbContext.Songs.Add(song);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(song);
    }

    // Add a list of songs to the database
    [HttpPost]
    public string AddSongList([FromBody] List<Song> songs)
    {
        // Add the song to the database
        
        dbContext.Songs.AddRange(songs);
        dbContext.SaveChanges();
        return "Songs Added";
    }

    // Get a list of all songs in the database
    [HttpGet]
    public JsonResult GetAllSongs()
    {
        return Json(dbContext.Songs.ToList());
    }

    // Download a song from the database
    [HttpGet]
    public async Task<IActionResult> DownloadAll()
    {
        var list = dbContext.Songs.Where(s => !s.Downloaded);
        foreach (var song in list)
        {
            song.Download(config["DownloadPath"]);
            dbContext.Songs.Update(song);
            await dbContext.SaveChangesAsync();
        }

        return Ok(new { success = true, message = "Songs Downloaded" });
    }

    // Download an individual song based off of its Id.
    [HttpPost]
    public async Task<IActionResult> Download(int id)
    {
        var song = dbContext.Songs.FirstOrDefault(s => s.Id == id);
        if (song == null)
            return Ok(new { success = false, message = "Song not found" });
        var res = song.Download(config["DownloadPath"]);
        if (!res)
        {
            song.Downloaded = true;
            dbContext.Songs.Update(song);
            await dbContext.SaveChangesAsync();
            return Ok(new { success = false, message = "Song file already exists" });
        }

        dbContext.Songs.Update(song);
        dbContext.SaveChanges();
        return Ok(new { success = true, message = "Song Downloaded" });
    }

    // Delete a song based off of its id.
    [HttpDelete]
    public void Delete(int id)
    {
        var song = dbContext.Songs.FirstOrDefault(s => s.Id == id);
        if (song == null)
            return;
        dbContext.Songs.Remove(song);
        dbContext.SaveChanges();
    }

    // Delete a list of songs based off of their ids.
    [HttpDelete]
    public void DeleteList([FromBody] List<int> ids)
    {
        var songs = dbContext.Songs.Where(s => ids.Contains(s.Id));
        if (songs == null)
            return;
        dbContext.Songs.RemoveRange(songs);
        dbContext.SaveChanges();
    }

    // Edit a song based off of its id.
    [HttpPost]
    public async Task<IActionResult> Edit(Song song)
    {
        if (ModelState.IsValid)
        {
            dbContext.Songs.Update(song);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(song);
    }
}