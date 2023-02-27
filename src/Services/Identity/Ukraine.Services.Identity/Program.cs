using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ukraine.Framework.Core.Configuration;
using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Host;
using Ukraine.Framework.Core.Options;
using Ukraine.Framework.Core.Serilog;
using Ukraine.Framework.Dapr;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Identity.Models;
using Ukraine.Services.Identity.Persistence;
using IdentityOptions = Ukraine.Services.Identity.Options.IdentityOptions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var isDevelopment = builder.Environment.IsDevelopment();

configuration.AddDaprSecretStore("ukraine-secretstore");

builder.Host.UseSerilog(configuration);
builder.Host.AddServicesValidationOnBuild();

var connectionString = builder.Configuration.GetRequiredConnectionString("Postgres");

services.AddRazorPages();

services.AddDbContext<UkraineIdentityContext>(dbBuilder =>
{
	dbBuilder.UseNpgsql(connectionString, sqlOptions =>
	{
		sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
		sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_example");
	});

	dbBuilder.UseSnakeCaseNamingConvention();
});

services.AddDatabaseFacadeResolver<UkraineIdentityContext>();
services.TryAddScoped<DbContext, UkraineIdentityContext>();

var identityOptions = builder.Configuration.GetRequiredSection(IdentityOptions.SECTION_NAME).GetOptions<IdentityOptions>();

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
		configurationStoreOptions.DefaultSchema = "ukraine_identity_configuration";
		configurationStoreOptions.ConfigureDbContext = b =>
		{
			b.UseSnakeCaseNamingConvention();
			b.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
				sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_configuration");
			});
		};
	})
	.AddOperationalStore(operationalStoreOptions =>
	{
		operationalStoreOptions.DefaultSchema = "ukraine_identity_operational";
		operationalStoreOptions.ConfigureDbContext = b =>
		{
			b.UseSnakeCaseNamingConvention();
			b.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
				sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_operational");
			});
		};
	});

if (isDevelopment)
	 identitySever.AddDeveloperSigningCredential();

services.AddAuthentication();

services
	.AddHealthChecks()
	.AddCheck(
		"Identity Server",
		() => HealthCheckResult.Healthy(),
		new[] { "service", "identity" })
	.AddNpgSql(
		connectionString,
		name: "Postgres Database",
		tags: new[] { "database", "postgres" });

var app = builder.Build();

if (isDevelopment)
{
	app.UseDeveloperExceptionPage();

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

	app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

// Fix login issues with Chrome 80+ using HTTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();
app.MapDefaultHealthChecks();

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