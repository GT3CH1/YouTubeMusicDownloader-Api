using System.ComponentModel.DataAnnotations;
using NYoutubeDL;
using NYoutubeDL.Helpers;

namespace YouTubeDownloader.Models;

public class Song
{
    [Display(Name = "Song URL")]
    [RegularExpression("^(http(s)?:\\/\\/)?((w){3}.)?youtu(be|.be)?(\\.com)?\\/watch\\?v=.+", ErrorMessage = "Please enter a valid URL")]
    [Required]
    public string Url { get; set; }

    [Display(Name = "Song Title")]
    [Required]
    public string Title { get; set; }

    [Display(Name = "Song Artist")]
    [Required]public string Artist { get; set; }

    [Display(Name = "Song Album")]
    [Required]public string Album { get; set; }

    public bool Downloaded { get; set; }
    [Key] public int Id { get; set; }

    /// <summary>
    ///     Gets the title without punctuation or symbols.
    /// </summary>
    /// <returns>The title without punctuation.</returns>
    public string GetTitleWithoutPunctuation()
    {
        return new(Title.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());
    }

    /// <summary>
    ///     Gets the artist without punctuation or symbols.
    /// </summary>
    /// <returns>The artist without punctuation.</returns>
    public string GetArtistWithoutPunctuation()
    {
        return new(Artist.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());
    }

    /// <summary>
    ///     Gets the album without punctuation or symbols.
    /// </summary>
    /// <returns>The album without punctuation.</returns>
    public string GetAlbumWithoutPunctuation()
    {
        return new(Album.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());
    }

    /// <summary>
    ///     Downloads this song using youtube-dl.
    /// </summary>
    /// <returns>True if the song was successfully downloaded.</returns>
    public bool Download(string songPath)
    {
        // Remove punctuation from the song title using LINQ
        Title = new string(GetTitleWithoutPunctuation());
        // Remove punctuation from the song artist using LINQ
        Artist = new string(GetArtistWithoutPunctuation());
        // Remove punctuation from the song album using LINQ
        Album = new string(GetAlbumWithoutPunctuation());

        var youtubeDl = new YoutubeDL();

        youtubeDl.Options.PostProcessingOptions.AudioFormat = Enums.AudioFormat.m4a;
        // Extract audio
        youtubeDl.Options.PostProcessingOptions.ExtractAudio = true;
        youtubeDl.Options.VideoFormatOptions.Format = Enums.VideoFormat.best;
        var dir = Path.Combine(songPath, Artist, Album);
        var path = Path.Combine(dir, Title + ".m4a");
        Directory.CreateDirectory(dir);
        // Set output directory
        youtubeDl.Options.FilesystemOptions.Output = path;
        // Set write meta data
        youtubeDl.Options.PostProcessingOptions.AddMetadata = true;
        // Prefer ffmpeg
        youtubeDl.Options.PostProcessingOptions.PreferFfmpeg = true;

        // Download the song
        youtubeDl.VideoUrl = Url;
        // Check if path exists, if does return false.
        if (File.Exists(path))
            return false;

        Console.WriteLine($"Downloading {Title}");
        youtubeDl.Download();
        var file = TagLib.File.Create(path);
        file.Tag.Title = Title;
        file.Tag.Performers = new[] { Artist };
        file.Tag.AlbumArtists = new[] { Artist };
        file.Tag.Album = Album;
        file.Save();
        Downloaded = true;
        return true;
    }
}