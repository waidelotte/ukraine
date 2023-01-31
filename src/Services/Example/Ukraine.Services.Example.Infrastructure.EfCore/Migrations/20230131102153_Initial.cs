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
                name: "example_entity",
                schema: "example_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stringvalue = table.Column<string>(name: "string_value", type: "text", nullable: true),
                    intvalue = table.Column<int>(name: "int_value", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_example_entity", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "example_entity",
                schema: "example_schema");
        }
    }
}
