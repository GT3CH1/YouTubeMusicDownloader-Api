using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeDownloader.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove Url being the primary key
            migrationBuilder.DropPrimaryKey(
                name: "Url",
                table: "Songs");
            // Add ID Row
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Songs",
                nullable: false,
                defaultValue: 0).Annotation("Sqlite:Autoincrement", true);
            // Set Id as the primary key.
            migrationBuilder.AddPrimaryKey(
                name:"PK_Id",
                table: "Songs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
