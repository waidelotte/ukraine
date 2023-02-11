using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ukraine.Services.Example.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Audit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_utc",
                schema: "example_schema",
                table: "books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_utc",
                schema: "example_schema",
                table: "books",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_utc",
                schema: "example_schema",
                table: "authors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modified_utc",
                schema: "example_schema",
                table: "authors",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_utc",
                schema: "example_schema",
                table: "books");

            migrationBuilder.DropColumn(
                name: "last_modified_utc",
                schema: "example_schema",
                table: "books");

            migrationBuilder.DropColumn(
                name: "created_utc",
                schema: "example_schema",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "last_modified_utc",
                schema: "example_schema",
                table: "authors");
        }
    }
}
