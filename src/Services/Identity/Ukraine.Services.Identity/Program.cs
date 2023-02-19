using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Serilog.Extenstion;
using Ukraine.Persistence.EfCore.Extensions;
using Ukraine.Persistence.EfCore.Interfaces;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Services.Identity;
using Ukraine.Services.Identity.Exceptions;
using Ukraine.Services.Identity.Models;
using Ukraine.Services.Identity.Options;
using Ukraine.Services.Identity.Persistence;
using IdentityOptions = Ukraine.Services.Identity.Options.IdentityOptions;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

var connectionString = builder.Configuration.GetConnectionString("Postgres");

if (string.IsNullOrEmpty(connectionString))
	throw IdentityException.Exception("Configuration: Postgres Connection String is null or empty");

var loggingOptions = builder.Configuration.GetRequiredSection<IdentityLoggingOptions>(IdentityLoggingOptions.SECTION_NAME);
var databaseOptions = builder.Configuration.GetRequiredSection<IdentityDatabaseOptions>(IdentityDatabaseOptions.SECTION_NAME);
var identityOptions = builder.Configuration.GetRequiredSection<IdentityOptions>(IdentityOptions.SECTION_NAME);

builder.Host.UseUkraineSerilog(options =>
{
	options.ServiceName = Constants.SERVICE_NAME;
	options.MinimumLevel = loggingOptions.MinimumLevel;
	options.Override(loggingOptions.Override);

	options.WriteTo = writeOptions =>
	{
		writeOptions.WriteToSeqServerUrl = loggingOptions.WriteToSeqServerUrl;
	};
});

builder.Host.UseUkraineServicesValidationOnBuild();

builder.Services.AddRazorPages();
builder.Services.AddUkrainePostgresContext<UkraineIdentityContext, UkraineIdentityContext>(connectionString, options =>
{
	options.RetryOnFailureDelay = databaseOptions.RetryOnFailureDelay;
	options.RetryOnFailureCount = databaseOptions.RetryOnFailureCount;
	options.DetailedErrors = databaseOptions.DetailedErrors;
	options.SensitiveDataLogging = databaseOptions.SensitiveDataLogging;
});

builder.Services
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
	.AddEntityFrameworkStores<UkraineIdentityContext>();

var identitySever = builder.Services.AddIdentityServer(options =>
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

builder.Services.AddAuthentication();

builder.Services
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
	app.Logger.LogInformation("Starting Web Host [{ServiceName}]", Constants.SERVICE_NAME);
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [{ServiceName}]", Constants.SERVICE_NAME);
}
finally
{
	Serilog.Log.CloseAndFlush();
}