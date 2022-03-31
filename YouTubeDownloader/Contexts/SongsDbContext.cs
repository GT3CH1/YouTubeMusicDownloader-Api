using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.Contexts;

public class SongsDbContext : DbContext
{ 
    // The connection string
    public string ConnectionString { get; set; }
    public SongsDbContext(DbContextOptions<SongsDbContext> options) : base(options)
    {
        
    }
    public DbSet<Song> Songs { get; set; }

}