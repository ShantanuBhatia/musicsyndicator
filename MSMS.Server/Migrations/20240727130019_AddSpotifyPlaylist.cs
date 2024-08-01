using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSpotifyPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpotifyLists",
                columns: table => new
                {
                    SpotifyPlaylistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SpotifyPlaylistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ArtistListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpotifyLists", x => x.SpotifyPlaylistId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpotifyLists");
        }
    }
}
