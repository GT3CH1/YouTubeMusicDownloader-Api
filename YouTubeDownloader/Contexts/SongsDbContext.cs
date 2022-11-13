using Microsoft.EntityFrameworkCore;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.Contexts;

public class SongsDbContext : DbContext
{
    public SongsDbContext(DbContextOptions<SongsDbContext> options) : base(options)
    {
        if (Database.GetPendingMigrations().Any()) Database.Migrate();
    }

    // The connection string
    public string ConnectionString { get; set; }
    public DbSet<Song> Songs { get; set; }
}