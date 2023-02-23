using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ukraine.Services.Identity.Persistence.Migrations.IdentityOperational
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ukraine_identity_operational");

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                schema: "ukraine_identity_operational",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    device_code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    subject_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_codes", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                schema: "ukraine_identity_operational",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    use = table.Column<string>(type: "text", nullable: true),
                    algorithm = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_x509certificate = table.Column<bool>(type: "boolean", nullable: false),
                    data_protected = table.Column<bool>(type: "boolean", nullable: false),
                    data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                schema: "ukraine_identity_operational",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    subject_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    consumed_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persisted_grants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServerSideSessions",
                schema: "ukraine_identity_operational",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    scheme = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    subject_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    session_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    display_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    renewed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_server_side_sessions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_device_codes_device_code",
                schema: "ukraine_identity_operational",
                table: "DeviceCodes",
                column: "device_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_device_codes_expiration",
                schema: "ukraine_identity_operational",
                table: "DeviceCodes",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_keys_use",
                schema: "ukraine_identity_operational",
                table: "Keys",
                column: "use");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_consumed_time",
                schema: "ukraine_identity_operational",
                table: "PersistedGrants",
                column: "consumed_time");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_expiration",
                schema: "ukraine_identity_operational",
                table: "PersistedGrants",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_key",
                schema: "ukraine_identity_operational",
                table: "PersistedGrants",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_subject_id_client_id_type",
                schema: "ukraine_identity_operational",
                table: "PersistedGrants",
                columns: new[] { "subject_id", "client_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_subject_id_session_id_type",
                schema: "ukraine_identity_operational",
                table: "PersistedGrants",
                columns: new[] { "subject_id", "session_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_display_name",
                schema: "ukraine_identity_operational",
                table: "ServerSideSessions",
                column: "display_name");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_expires",
                schema: "ukraine_identity_operational",
                table: "ServerSideSessions",
                column: "expires");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_key",
                schema: "ukraine_identity_operational",
                table: "ServerSideSessions",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_session_id",
                schema: "ukraine_identity_operational",
                table: "ServerSideSessions",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_subject_id",
                schema: "ukraine_identity_operational",
                table: "ServerSideSessions",
                column: "subject_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes",
                schema: "ukraine_identity_operational");

            migrationBuilder.DropTable(
                name: "Keys",
                schema: "ukraine_identity_operational");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "ukraine_identity_operational");

            migrationBuilder.DropTable(
                name: "ServerSideSessions",
                schema: "ukraine_identity_operational");
        }
    }
}
