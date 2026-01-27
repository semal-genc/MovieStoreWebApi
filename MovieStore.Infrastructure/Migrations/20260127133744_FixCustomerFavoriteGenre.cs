using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCustomerFavoriteGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFavoriteGenre_Customers_CustomerId",
                table: "CustomerFavoriteGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFavoriteGenre_Genres_GenreId",
                table: "CustomerFavoriteGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerFavoriteGenre",
                table: "CustomerFavoriteGenre");

            migrationBuilder.RenameTable(
                name: "CustomerFavoriteGenre",
                newName: "CustomerFavoriteGenres");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerFavoriteGenre_GenreId",
                table: "CustomerFavoriteGenres",
                newName: "IX_CustomerFavoriteGenres_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerFavoriteGenres",
                table: "CustomerFavoriteGenres",
                columns: new[] { "CustomerId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFavoriteGenres_Customers_CustomerId",
                table: "CustomerFavoriteGenres",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFavoriteGenres_Genres_GenreId",
                table: "CustomerFavoriteGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFavoriteGenres_Customers_CustomerId",
                table: "CustomerFavoriteGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerFavoriteGenres_Genres_GenreId",
                table: "CustomerFavoriteGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerFavoriteGenres",
                table: "CustomerFavoriteGenres");

            migrationBuilder.RenameTable(
                name: "CustomerFavoriteGenres",
                newName: "CustomerFavoriteGenre");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerFavoriteGenres_GenreId",
                table: "CustomerFavoriteGenre",
                newName: "IX_CustomerFavoriteGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerFavoriteGenre",
                table: "CustomerFavoriteGenre",
                columns: new[] { "CustomerId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFavoriteGenre_Customers_CustomerId",
                table: "CustomerFavoriteGenre",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerFavoriteGenre_Genres_GenreId",
                table: "CustomerFavoriteGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
