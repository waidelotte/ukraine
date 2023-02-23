using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ukraine.Core.Extensions;
using Ukraine.Dapr.Extensions;
using Ukraine.EfCore.Extensions;
using Ukraine.EfCore.Interfaces;
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
builder.Host.AddUkraineServicesValidationOnBuild();

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
	.AddInMemoryIdentityResources(new IdentityResource[]
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Profile()
	})
	.AddInMemoryApiResources(identityOptions.ApiResources
		.Select(o => new ApiResource(o.Name, o.DisplayName)
		{
			Scopes = new List<string>(o.Scopes)
		}))
	.AddInMemoryApiScopes(identityOptions.ApiScopes.Select(o => new ApiScope(o.Name, o.DisplayName)))
	.AddInMemoryClients(identityOptions.Clients.Select(o => new Client
	{
		ClientId = o.ClientId,
		ClientName = o.ClientName,
		AllowedGrantTypes = o.AllowedGrantTypes,
		AllowedScopes = o.AllowedScopes,
		AllowAccessTokensViaBrowser = o.AllowAccessTokensViaBrowser,
		RedirectUris = o.RedirectUris,
		PostLogoutRedirectUris = o.PostLogoutRedirectUris
	}))
	.AddAspNetIdentity<UkraineUser>();

if (isDevelopment)
	 identitySever.AddDeveloperSigningCredential();

services.AddAuthentication();

services
	.AddUkraineHealthChecks()
	.AddUkrainePostgresHealthCheck(connectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
	await context.Database.MigrateAsync();

	if (isDevelopment)
		await Seed.SeedDevAsync(scope);
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