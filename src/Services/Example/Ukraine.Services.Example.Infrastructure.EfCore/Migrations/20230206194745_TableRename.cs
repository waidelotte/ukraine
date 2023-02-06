using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ukraine.Services.Example.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class TableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_example_entities_author_id",
                schema: "example_schema",
                table: "book");

            migrationBuilder.DropPrimaryKey(
                name: "pk_example_entities",
                schema: "example_schema",
                table: "example_entities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_book",
                schema: "example_schema",
                table: "book");

            migrationBuilder.RenameTable(
                name: "example_entities",
                schema: "example_schema",
                newName: "authors",
                newSchema: "example_schema");

            migrationBuilder.RenameTable(
                name: "book",
                schema: "example_schema",
                newName: "books",
                newSchema: "example_schema");

            migrationBuilder.RenameIndex(
                name: "ix_book_author_id",
                schema: "example_schema",
                table: "books",
                newName: "ix_books_author_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_authors",
                schema: "example_schema",
                table: "authors",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_books",
                schema: "example_schema",
                table: "books",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_books_authors_author_id",
                schema: "example_schema",
                table: "books",
                column: "author_id",
                principalSchema: "example_schema",
                principalTable: "authors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_books_authors_author_id",
                schema: "example_schema",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "pk_books",
                schema: "example_schema",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "pk_authors",
                schema: "example_schema",
                table: "authors");

            migrationBuilder.RenameTable(
                name: "books",
                schema: "example_schema",
                newName: "book",
                newSchema: "example_schema");

            migrationBuilder.RenameTable(
                name: "authors",
                schema: "example_schema",
                newName: "example_entities",
                newSchema: "example_schema");

            migrationBuilder.RenameIndex(
                name: "ix_books_author_id",
                schema: "example_schema",
                table: "book",
                newName: "ix_book_author_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_book",
                schema: "example_schema",
                table: "book",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_example_entities",
                schema: "example_schema",
                table: "example_entities",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_book_example_entities_author_id",
                schema: "example_schema",
                table: "book",
                column: "author_id",
                principalSchema: "example_schema",
                principalTable: "example_entities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
