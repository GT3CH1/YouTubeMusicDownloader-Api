using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeDownloader.Migrations
{
    public partial class AddDownloadedFlagToSongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Downloaded",
                table: "Songs",
                nullable: false,
                defaultValue: false);
        }
    }
}
