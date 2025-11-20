using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteLists",
                columns: table => new
                {
                    FavouriteListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListsBelongsToUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMovieOrActor = table.Column<bool>(type: "bit", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    ListCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ListDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReported = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLists", x => x.FavouriteListID);
                });

            migrationBuilder.CreateTable(
                name: "FilesToDatabase",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ImageID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteLists");

            migrationBuilder.DropTable(
                name: "FilesToDatabase");
        }
    }
}
