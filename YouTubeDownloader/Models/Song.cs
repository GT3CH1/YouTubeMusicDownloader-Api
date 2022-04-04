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
}