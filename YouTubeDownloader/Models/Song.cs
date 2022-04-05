using System.ComponentModel.DataAnnotations;

namespace YouTubeDownloader.Models;

public class Song
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    private int Downloaded { get; set; }
    [Key] 
    public int Id { get; set; }

    public bool IsDownloaded() => Downloaded == 1;
    public void SetDownloaded() => Downloaded = 1;

    /// <summary>
    /// Gets the title without punctuation or symbols.
    /// </summary>
    /// <returns>The title without punctuation.</returns>
    public string GetTitleWithoutPunctuation() =>
        new string(Title.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());
    
    /// <summary>
    /// Gets the artist without punctuation or symbols.
    /// </summary>
    /// <returns>The artist without punctuation.</returns>
    public string GetArtistWithoutPunctuation() =>
        new string(Artist.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());
    
    /// <summary>
    /// Gets the album without punctuation or symbols.
    /// </summary>
    /// <returns>The album wtihout punctuation.</returns>
    public string GetAlbumWithoutPunctuation() =>
        new string(Album.Where(c => !(char.IsPunctuation(c) || char.IsSymbol(c))).ToArray());

}