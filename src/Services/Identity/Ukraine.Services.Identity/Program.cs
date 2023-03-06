using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.EntityFramework.Storage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Ukraine.Framework.Core.Configuration;
using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Options;
using Ukraine.Framework.Core.Serilog;
using Ukraine.Services.Identity.Persistence.Configuration;
using Ukraine.Services.Identity.Persistence.DbContexts;
using Ukraine.Services.Identity.Persistence.Entities;
using Ukraine.Services.Identity.Persistence.Helpers;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.AddJsonFile($"Seed/identity_data.{builder.Environment.EnvironmentName}.json", true, false);
configuration.AddJsonFile($"Seed/identity_server_data.{builder.Environment.EnvironmentName}.json", true, false);

var services = builder.Services;

builder.Host.UseSerilog(configuration);

services.AddSingleton(configuration.GetRequiredSection(nameof(IdentityData)).GetOptions<IdentityData>());
services.AddSingleton(configuration.GetRequiredSection(nameof(IdentityServerData)).GetOptions<IdentityServerData>());

var identityConnectionString = configuration.GetRequiredConnectionString("IdentityDbConnection");
var configurationConnectionString = configuration.GetRequiredConnectionString("ConfigurationDbConnection");
var persistedGrantsConnectionString = configuration.GetRequiredConnectionString("PersistedGrantDbConnection");
var dataProtectionConnectionString = configuration.GetRequiredConnectionString("DataProtectionDbConnection");

services.AddDbContext<ServiceIdentityDbContext>(options => options.UseNpgsql(identityConnectionString));
services.AddDbContext<IdentityServerDataProtectionDbContext>(options => options.UseNpgsql(dataProtectionConnectionString));
services.AddConfigurationDbContext<IdentityServerConfigurationDbContext>(
	options => options.ConfigureDbContext = b => b.UseNpgsql(configurationConnectionString));
services.AddOperationalDbContext<IdentityServerPersistedGrantDbContext>(
	options => options.ConfigureDbContext = b => b.UseNpgsql(persistedGrantsConnectionString));

services
	.AddDataProtection()
	.SetApplicationName("Ukraine.IdentityServer")
	.PersistKeysToDbContext<IdentityServerDataProtectionDbContext>();

services
	.AddIdentity<UserIdentity, UserIdentityRole>(options => configuration.GetSection(nameof(IdentityOptions)).Bind(options))
	.AddEntityFrameworkStores<ServiceIdentityDbContext>()
	.AddDefaultTokenProviders();

services.Configure<CookiePolicyOptions>(options =>
{
	options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
	options.Secure = CookieSecurePolicy.SameAsRequest;
});

var configurationSection = configuration.GetSection(nameof(IdentityServerOptions));

services.AddIdentityServer(options =>
	{
		configurationSection.Bind(options);
	})
	.AddConfigurationStore<IdentityServerConfigurationDbContext>()
	.AddOperationalStore<IdentityServerPersistedGrantDbContext>()
	.AddAspNetIdentity<UserIdentity>();

services.AddControllersWithViews();

services.AddAuthorization();

services
	.AddHealthChecks()
	.AddDbContextCheck<IdentityServerConfigurationDbContext>("ConfigurationDbContext")
	.AddDbContextCheck<IdentityServerPersistedGrantDbContext>("PersistedGrantsDbContext")
	.AddDbContextCheck<ServiceIdentityDbContext>("IdentityDbContext")
	.AddDbContextCheck<IdentityServerDataProtectionDbContext>("DataProtectionDbContext")
	.AddNpgSql(
		configurationConnectionString,
		name: "ConfigurationDb",
		healthQuery: "SELECT * FROM \"ApiResources\" LIMIT 1")
	.AddNpgSql(
		persistedGrantsConnectionString,
		name: "PersistentGrantsDb",
		healthQuery: "SELECT * FROM \"PersistedGrants\" LIMIT 1")
	.AddNpgSql(
		identityConnectionString,
		name: "IdentityDb",
		healthQuery: "SELECT * FROM \"Users\" LIMIT 1")
	.AddNpgSql(
		dataProtectionConnectionString,
		name: "DataProtectionDb",
		healthQuery: "SELECT * FROM \"DataProtectionKeys\" LIMIT 1");

var app = builder.Build();

await app.ApplyMigrationsWithDataSeedAsync();

app.UseCookiePolicy();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseIdentityServer();
app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapDefaultHealthChecks();

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