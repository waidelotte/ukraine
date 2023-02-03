using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ukraine.Services.Example.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class ExampleChildEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "example_child_entity",
                schema: "example_schema",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notnullintvalue = table.Column<int>(name: "not_null_int_value", type: "integer", nullable: false),
                    exampleentityid = table.Column<Guid>(name: "example_entity_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_example_child_entity", x => x.id);
                    table.ForeignKey(
                        name: "fk_example_child_entity_example_entities_example_entity_id",
                        column: x => x.exampleentityid,
                        principalSchema: "example_schema",
                        principalTable: "example_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_example_child_entity_example_entity_id",
                schema: "example_schema",
                table: "example_child_entity",
                column: "example_entity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "example_child_entity",
                schema: "example_schema");
        }
    }
}
