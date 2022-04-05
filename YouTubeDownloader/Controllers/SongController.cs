using Microsoft.AspNetCore.Mvc;
using NYoutubeDL;
using NYoutubeDL.Helpers;
using TagLib;
using YouTubeDownloader.Contexts;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.Controllers;

[Route("[controller]")]
public class SongController : Controller
{
    private SongsDbContext dbContext;

    public SongController(SongsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET
    public IActionResult Index()
    {
        return View(dbContext.Songs.ToList());
    }

    // Add a Song to the Database
    [HttpPost]
    [Route("Add")]
    public IActionResult AddSong([FromForm] Song song)
    {
        // Check to see if the song already exists
        if (!dbContext.Songs.Any(s => s.Url == song.Url))
        {
            dbContext.Songs.Add(song);
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
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

    // Download a song from the database
    [HttpGet]
    [Route("DownloadAll")]
    public void DownloadAll()
    {
        var list = dbContext.Songs.ToList();
        foreach (var song in list)
        {
            DownloadSong(song);
            // Mark song as downloaded, update database
            song.SetDownloaded();
            dbContext.Songs.Update(song);
            dbContext.SaveChanges();
        }
    }

    // Download an individual song based off of its Id.
    [HttpGet]
    [Route("Download/{id}")]
    public void DownloadSong(int id)
    {
        var song = dbContext.Songs.FirstOrDefault(s => s.Id == id);
        if (song != null)
            DownloadSong(song);
        // Mark song as downloaded, update database
        song.SetDownloaded();
        dbContext.Songs.Update(song);
        dbContext.SaveChanges();
    }
    
    // Delete a song based off of its id.
    [HttpDelete]
    [Route("Delete/{id}")]
    public void DeleteSong(int id)
    {
        var song = dbContext.Songs.FirstOrDefault(s => s.Id == id);
        if (song != null)
        {
            dbContext.Songs.Remove(song);
            dbContext.SaveChanges();
        }
    }
    
    // Delete a list of songs based off of their ids.
    [HttpDelete]
    [Route("DeleteList")]
    public void DeleteSongList([FromBody] List<int> ids)
    {
        var songs = dbContext.Songs.Where(s => ids.Contains(s.Id));
        if (songs != null)
        {
            dbContext.Songs.RemoveRange(songs);
            dbContext.SaveChanges();
        }
    }

    private void DownloadSong(Song song)
    {
        // Remove punctuation from the song title using LINQ
        song.Title = new string(song.GetTitleWithoutPunctuation());
        // Remove punctuation from the song artist using LINQ
        song.Artist = new string(song.GetArtistWithoutPunctuation());
        // Remove punctuation from the song album using LINQ
        song.Album = new string(song.GetAlbumWithoutPunctuation());

        var youtubeDl = new YoutubeDL();

        youtubeDl.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.m4a;
        // Extract audio
        youtubeDl.Options.PostProcessingOptions.ExtractAudio = true;
        youtubeDl.Options.VideoFormatOptions.Format = Enums.VideoFormat.best;
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "Songs", song.Artist, song.Album);
        var path = Path.Combine(dir, song.Title + ".m4a");
        Directory.CreateDirectory(dir);
        // Set output directory
        youtubeDl.Options.FilesystemOptions.Output = path;
        // Set write meta data
        youtubeDl.Options.PostProcessingOptions.AddMetadata = true;
        // Prefer ffmpeg
        youtubeDl.Options.PostProcessingOptions.PreferFfmpeg = true;

        // Download the song
        youtubeDl.VideoUrl = song.Url;
        if (!System.IO.File.Exists(path))
        {
            Console.WriteLine($"Downloading {song.Title}");
            youtubeDl.Download();
        }

        var file = TagLib.File.Create(path);
        file.Tag.Title = song.Title;
        file.Tag.Performers = new[] { song.Artist };
        file.Tag.AlbumArtists = new[] { song.Artist };
        file.Tag.Album = song.Album;
        file.Save();
    }
}