using Microsoft.AspNetCore.Mvc;
using NYoutubeDL;
using NYoutubeDL.Helpers;
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

    // Download a song from the database
    [HttpGet]
    [Route("DownloadAll")]
    public void DownloadAll()
    {
        var list = dbContext.Songs.ToList();
        foreach (var song in list)
        {
            DownloadSong(song);
        }
    }

    private void DownloadSong(Song song)
    {
        var youtubeDl = new YoutubeDLP();
        // Set youtubedl path to /usr/bin/youtube-dl
        youtubeDl.YoutubeDlPath = @"C:\Users\gcpease\.bin\youtube-dl.exe";
        // Set audio to best
        youtubeDl.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.mp3;
        // Extract audio
        youtubeDl.Options.PostProcessingOptions.ExtractAudio = true;
        // Make Artist/Album directories
        // Create path to file
        var dir = $"Songs/{song.Artist}/{song.Album}";
        var path = $"{dir}/{song.Title}.mp3";
        Directory.CreateDirectory(dir);
        // Set output directory
        youtubeDl.Options.FilesystemOptions.Output = path;
        // Download the song
        youtubeDl.VideoUrl = song.Url;
        if (!System.IO.File.Exists(path))
            youtubeDl.Download(song.Url);
        // Tag the song.
        var file = TagLib.File.Create(path);
        file.Tag.Album = song.Album;
        file.Tag.Title = song.Title;
        file.Tag.AlbumArtists = new[] { song.Artist };
        file.Save();
    }
}