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
                name: "example_entities",
                schema: "example_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: true),
                    supersecretkey = table.Column<Guid>(name: "super_secret_key", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_example_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book",
                schema: "example_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    authorid = table.Column<Guid>(name: "author_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_example_entities_author_id",
                        column: x => x.authorid,
                        principalSchema: "example_schema",
                        principalTable: "example_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_author_id",
                schema: "example_schema",
                table: "book",
                column: "author_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book",
                schema: "example_schema");

            migrationBuilder.DropTable(
                name: "example_entities",
                schema: "example_schema");
        }
    }
}
