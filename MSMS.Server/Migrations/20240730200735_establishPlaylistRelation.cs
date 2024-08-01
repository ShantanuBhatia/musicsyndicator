using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class establishPlaylistRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SpotifyLists_ArtistListId",
                table: "SpotifyLists",
                column: "ArtistListId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpotifyLists_ArtistLists_ArtistListId",
                table: "SpotifyLists",
                column: "ArtistListId",
                principalTable: "ArtistLists",
                principalColumn: "ArtistListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpotifyLists_ArtistLists_ArtistListId",
                table: "SpotifyLists");

            migrationBuilder.DropIndex(
                name: "IX_SpotifyLists_ArtistListId",
                table: "SpotifyLists");
        }
    }
}
