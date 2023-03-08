#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ukraine.Services.Identity.Persistence.Migrations.DataProtection;

/// <inheritdoc />
public partial class Initial : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			"DataProtectionKeys",
			table => new
			{
				Id = table.Column<int>("integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy",
						NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				FriendlyName = table.Column<string>("text", nullable: true),
				Xml = table.Column<string>("text", nullable: true)
			},
			constraints: table => { table.PrimaryKey("PK_DataProtectionKeys", x => x.Id); });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			"DataProtectionKeys");
	}
}