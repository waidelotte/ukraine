using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Ukraine.Core.Extensions;
using Ukraine.Dapr.Extensions;
using Ukraine.EfCore.Extensions;
using Ukraine.HealthChecks.Extenstion;
using Ukraine.Logging.Extenstion;
using Ukraine.Services.Identity.Exceptions;
using Ukraine.Services.Identity.Models;
using Ukraine.Services.Identity.Persistence;
using IdentityOptions = Ukraine.Services.Identity.Options.IdentityOptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var isDevelopment = builder.Environment.IsDevelopment();

configuration.AddUkraineDaprSecretStore("ukraine-secretstore");

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddServicesValidationOnBuild();

var connectionString = builder.Configuration.GetConnectionString("Postgres");

if (string.IsNullOrEmpty(connectionString))
	throw IdentityException.Exception("Postgres Connection String is null or empty");

services.AddRazorPages();
services.AddUkrainePostgresContext<UkraineIdentityContext, UkraineIdentityContext>(
	connectionString,
	configuration.GetSection("UkrainePostgres"));

var identityOptions = builder.Configuration.GetRequiredSection(IdentityOptions.SECTION_NAME).Get<IdentityOptions>();

if (identityOptions == null)
	throw IdentityException.Exception($"Configuration Section [{IdentityOptions.SECTION_NAME}] is empty");

services
	.AddIdentity<UkraineUser, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedEmail = identityOptions.User.Email.RequireConfirmed;
		options.User.RequireUniqueEmail = identityOptions.User.Email.RequireUnique;
		options.Password.RequireDigit = identityOptions.User.Password.RequireDigit;
		options.Password.RequiredLength = identityOptions.User.Password.RequiredLength;
		options.Password.RequireUppercase = identityOptions.User.Password.RequireUppercase;
		options.Password.RequireLowercase = identityOptions.User.Password.RequireLowercase;
		options.Password.RequireNonAlphanumeric = identityOptions.User.Password.RequireNonAlphanumeric;
	})
	.AddDefaultTokenProviders()
	.AddEntityFrameworkStores<UkraineIdentityContext>();

var identitySever = builder.Services
	.AddIdentityServer(options =>
	{
		options.IssuerUri = identityOptions.IssuerUri;
		options.Authentication.CookieLifetime = identityOptions.CookieLifetime;
		options.EmitStaticAudienceClaim = identityOptions.EmitStaticAudienceClaim;
		options.Events.RaiseErrorEvents = identityOptions.RaiseErrorEvents;
		options.Events.RaiseInformationEvents = identityOptions.RaiseInformationEvents;
		options.Events.RaiseFailureEvents = identityOptions.RaiseFailureEvents;
		options.Events.RaiseSuccessEvents = identityOptions.RaiseSuccessEvents;
	})
	.AddAspNetIdentity<UkraineUser>()
	.AddConfigurationStore(configurationStoreOptions =>
	{
		configurationStoreOptions.DefaultSchema = identityOptions.ConfigurationSchema;
		configurationStoreOptions.ConfigureDbContext = b =>
		{
			b.UseUkraineNamingConvention();
			b.UseUkrainePostgres<Program>(connectionString, identityOptions.ConfigurationSchema);
		};
	})
	.AddOperationalStore(operationalStoreOptions =>
	{
		operationalStoreOptions.DefaultSchema = identityOptions.OperationalSchema;
		operationalStoreOptions.ConfigureDbContext = b =>
		{
			b.UseUkraineNamingConvention();
			b.UseUkrainePostgres<Program>(connectionString, identityOptions.OperationalSchema);
		};
	});

if (isDevelopment)
	 identitySever.AddDeveloperSigningCredential();

services.AddAuthentication();

services
	.AddUkraineHealthChecks()
	.AddUkrainePostgresHealthCheck(connectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	await scope.MigrateDatabaseAsync<UkraineIdentityContext>();
	await scope.MigrateDatabaseAsync<ConfigurationDbContext>();
	await scope.MigrateDatabaseAsync<PersistedGrantDbContext>();

	if (isDevelopment)
	{
		await scope.SeedAdminRoleAsync();
		await scope.SeedDevUserAsync();
		await scope.SeedApiScopesAsync();
		await scope.SeedApiResourcesAsync();
		await scope.SeedIdentityResourcesAsync();
		await scope.SeedClientsAsync();
	}
}

if (isDevelopment)
	app.UseDeveloperExceptionPage();

app.UseStaticFiles();

// Fix login issues with Chrome 80+ using HTTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();
app.UseUkraineHealthChecks();
app.UseUkraineDatabaseHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [service-identity]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [service-identity]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}