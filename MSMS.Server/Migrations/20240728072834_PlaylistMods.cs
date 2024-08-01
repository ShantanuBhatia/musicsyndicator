using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class PlaylistMods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SpotifyLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PlaylistKey",
                table: "SpotifyLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaylistKey",
                table: "SpotifyLists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SpotifyLists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
