using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ukraine.Services.Example.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "example_schema");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "example_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: true),
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
                schema: "example_schema",
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
                        principalSchema: "example_schema",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_books_author_id",
                schema: "example_schema",
                table: "books",
                column: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books",
                schema: "example_schema");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "example_schema");
        }
    }
}
