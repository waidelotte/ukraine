using Serilog;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Configuration.Configuration;
using Skoruba.Duende.IdentityServer.Admin.UI.Helpers.ApplicationBuilder;
using Skoruba.Duende.IdentityServer.Admin.UI.Helpers.DependencyInjection;
using Ukraine.Services.Identity.Core.DTOs;
using Ukraine.Services.Identity.Persistence;
using Ukraine.Services.Identity.Persistence.DbContexts;
using Ukraine.Services.Identity.Persistence.Entities;
using Ukraine.Services.Identity.Persistence.Helpers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var loggerConfiguration = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration);

Log.Logger = loggerConfiguration.CreateLogger();

builder.Host.UseSerilog();

services.AddIdentityServerAdminUI<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext,
	AdminLogDbContext, AdminAuditLogDbContext, AuditLog, IdentityServerDataProtectionDbContext,
	UserIdentity, UserIdentityRole, UserIdentityUserClaim, UserIdentityUserRole,
	UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken, string,
	IdentityUserDTO, IdentityRoleDTO, IdentityUsersDTO, IdentityRolesDTO, IdentityUserRolesDTO,
	IdentityUserClaimsDTO, IdentityUserProviderDTO, IdentityUserProvidersDTO, IdentityUserChangePasswordDTO,
	IdentityRoleClaimsDTO, IdentityUserClaimDTO, IdentityRoleClaimDTO>(options =>
{
	options.BindConfiguration(configuration);
	options.Security.UseDeveloperExceptionPage = builder.Environment.IsDevelopment();
	options.DatabaseMigrations.SetMigrationsAssemblies(typeof(MigrationAssembly).Assembly.GetName().Name);
	options.Testing.IsStaging = builder.Environment.IsStaging() || builder.Environment.IsProduction();
});

var app = builder.Build();

await ApplyDbMigrationsWithDataSeedAsync(configuration, app);

app.UseRouting();
app.UseIdentityServerAdminUI();
app.MapIdentityServerAdminUI();
app.MapIdentityServerAdminUIHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [ukraine-identity]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [ukraine-identity]");
}
finally
{
	Log.CloseAndFlush();
}

static async Task ApplyDbMigrationsWithDataSeedAsync(IConfiguration configuration, IHost host)
{
	var seedConfiguration = configuration.GetSection(nameof(SeedConfiguration)).Get<SeedConfiguration>();
	var databaseMigrationsConfiguration = configuration.GetSection(nameof(DatabaseMigrationsConfiguration))
		.Get<DatabaseMigrationsConfiguration>();

	await DbMigrationHelpers
		.ApplyDbMigrationsWithDataSeedAsync<IdentityServerConfigurationDbContext, AdminIdentityDbContext,
			IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext,
			IdentityServerDataProtectionDbContext, UserIdentity, UserIdentityRole>(
			host,
			seedConfiguration,
			databaseMigrationsConfiguration);
}