using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class WFOIw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntryCreatedAt",
                table: "Movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryModifiedAt",
                table: "Movies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FilesToApi",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExistingFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isPoster = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToApi", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommenteryUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedScore = table.Column<int>(type: "int", nullable: false),
                    IsHelpful = table.Column<int>(type: "int", nullable: true),
                    IsHarmful = table.Column<int>(type: "int", nullable: true),
                    CommentCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.CommentID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesToApi");

            migrationBuilder.DropTable(
                name: "UserComments");

            migrationBuilder.DropColumn(
                name: "EntryCreatedAt",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "EntryModifiedAt",
                table: "Movies");
        }
    }
}
