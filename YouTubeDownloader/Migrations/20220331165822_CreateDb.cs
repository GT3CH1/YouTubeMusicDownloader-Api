using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeDownloader.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Songs",
            //     columns: table => new
            //     {
            //         Url = table.Column<string>(nullable: false),
            //         Artist = table.Column<string>(nullable: true),
            //         Title = table.Column<string>(nullable: true),
            //         Album = table.Column<string>(nullable: true),
            //     },
            //     constraints: table => {table.PrimaryKey("Url", x => x.Url);}
            //
            // );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
