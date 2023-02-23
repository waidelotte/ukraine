using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ukraine.Services.Example.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ukraine_example");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "ukraine_example",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    supersecretkey = table.Column<Guid>(name: "super_secret_key", type: "uuid", nullable: false),
                    createdutc = table.Column<DateTime>(name: "created_utc", type: "timestamp with time zone", nullable: false),
                    lastmodifiedutc = table.Column<DateTime>(name: "last_modified_utc", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                schema: "ukraine_example",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    authorid = table.Column<Guid>(name: "author_id", type: "uuid", nullable: false),
                    createdutc = table.Column<DateTime>(name: "created_utc", type: "timestamp with time zone", nullable: false),
                    lastmodifiedutc = table.Column<DateTime>(name: "last_modified_utc", type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                    table.ForeignKey(
                        name: "fk_books_authors_author_id",
                        column: x => x.authorid,
                        principalSchema: "ukraine_example",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_books_author_id",
                schema: "ukraine_example",
                table: "books",
                column: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "ukraine_example");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "ukraine_example");
        }
    }
}
