using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavouriteListID",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FavouriteListID",
                table: "Movies",
                column: "FavouriteListID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_FavouriteLists_FavouriteListID",
                table: "Movies",
                column: "FavouriteListID",
                principalTable: "FavouriteLists",
                principalColumn: "FavouriteListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_FavouriteLists_FavouriteListID",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_FavouriteListID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "FavouriteListID",
                table: "Movies");
        }
    }
}
