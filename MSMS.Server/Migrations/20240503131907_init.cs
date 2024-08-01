using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistLists",
                columns: table => new
                {
                    ArtistListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistLists", x => x.ArtistListId);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistSpotifyKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistArtistList",
                columns: table => new
                {
                    ArtistListsArtistListId = table.Column<int>(type: "int", nullable: false),
                    ArtistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistArtistList", x => new { x.ArtistListsArtistListId, x.ArtistsId });
                    table.ForeignKey(
                        name: "FK_ArtistArtistList_ArtistLists_ArtistListsArtistListId",
                        column: x => x.ArtistListsArtistListId,
                        principalTable: "ArtistLists",
                        principalColumn: "ArtistListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistArtistList_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistArtistList_ArtistsId",
                table: "ArtistArtistList",
                column: "ArtistsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistArtistList");

            migrationBuilder.DropTable(
                name: "ArtistLists");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
