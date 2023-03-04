using System.Globalization;
using Duende.IdentityServer.Configuration;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Serilog;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Configuration.PostgreSQL;
using Skoruba.Duende.IdentityServer.Shared.Configuration.Authentication;
using Skoruba.Duende.IdentityServer.Shared.Configuration.Configuration.Common;
using Skoruba.Duende.IdentityServer.Shared.Configuration.Configuration.Identity;
using Skoruba.Duende.IdentityServer.Shared.Configuration.Helpers;
using Ukraine.Services.Identity.Persistence.DbContexts;
using Ukraine.Services.Identity.Persistence.Entities;
using Ukraine.Services.Identity.Token.Configurations;
using Ukraine.Services.Identity.Token.Conventions;
using Ukraine.Services.Identity.Token.Localization;
using Ukraine.Services.Identity.Token.Managers;
using Ukraine.Services.Identity.Token.Validators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

var loggerConfiguration = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration);

Log.Logger = loggerConfiguration.CreateLogger();

builder.Host.UseSerilog();

var identityConnectionString = configuration.GetConnectionString("IdentityDbConnection");
var configurationConnectionString = configuration.GetConnectionString("ConfigurationDbConnection");
var persistedGrantsConnectionString = configuration.GetConnectionString("PersistedGrantDbConnection");
var dataProtectionConnectionString = configuration.GetConnectionString("DataProtectionDbConnection");

services.RegisterNpgSqlDbContexts<
	AdminIdentityDbContext,
	IdentityServerConfigurationDbContext,
	IdentityServerPersistedGrantDbContext,
	IdentityServerDataProtectionDbContext>(
	identityConnectionString,
	configurationConnectionString,
	persistedGrantsConnectionString,
	dataProtectionConnectionString);

services.AddDataProtection<IdentityServerDataProtectionDbContext>(
	new DataProtectionConfiguration
	{
		ProtectKeysWithAzureKeyVault = false
	},
	new AzureKeyVaultConfiguration());

services
	.AddSingleton(configuration.GetSection(nameof(RegisterConfiguration)).Get<RegisterConfiguration>())
	.AddSingleton(configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>())
	.AddScoped<ApplicationSignInManager<UserIdentity>>()
	.AddIdentity<UserIdentity, UserIdentityRole>(options => configuration.GetSection(nameof(IdentityOptions)).Bind(options))
	.AddEntityFrameworkStores<AdminIdentityDbContext>()
	.AddDefaultTokenProviders();

services.Configure<CookiePolicyOptions>(options =>
{
	options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
	options.Secure = CookieSecurePolicy.SameAsRequest;
	options.OnAppendCookie = cookieContext =>
		AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
	options.OnDeleteCookie = cookieContext =>
		AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
});

var configurationSection = configuration.GetSection(nameof(IdentityServerOptions));

services.AddIdentityServer(options =>
	{
		configurationSection.Bind(options);
		options.DynamicProviders.SignInScheme = IdentityConstants.ExternalScheme;
		options.DynamicProviders.SignOutScheme = IdentityConstants.ApplicationScheme;
	})
	.AddConfigurationStore<IdentityServerConfigurationDbContext>()
	.AddOperationalStore<IdentityServerPersistedGrantDbContext>()
	.AddAspNetIdentity<UserIdentity>()
	.AddExtensionGrantValidator<DelegationGrantValidator>();

services.ConfigureOptions<OpenIdClaimsMappingConfig>();

services.AddHsts(options =>
{
	options.Preload = true;
	options.IncludeSubDomains = true;
	options.MaxAge = TimeSpan.FromDays(365);
});

services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

services.TryAddTransient(typeof(IGenericControllerLocalizer<>), typeof(GenericControllerLocalizer<>));

services.AddControllersWithViews(o =>
	{
		o.Conventions.Add(new GenericControllerRouteConvention());
	})
	.AddViewLocalization(
		LanguageViewLocationExpanderFormat.Suffix,
		opts => { opts.ResourcesPath = "Resources"; })
	.AddDataAnnotationsLocalization()
	.ConfigureApplicationPartManager(m =>
	{
		m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider<UserIdentity, string>());
	});

services.Configure<RequestLocalizationOptions>(
	opts =>
	{
		opts.DefaultRequestCulture = new RequestCulture("en");
		opts.SupportedCultures = new List<CultureInfo> { new("en") };
		opts.SupportedUICultures = new List<CultureInfo> { new("en") };
	});

services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("admin"));
});


var healthChecksBuilder = services
	.AddHealthChecks()
	.AddDbContextCheck<IdentityServerConfigurationDbContext>("ConfigurationDbContext")
	.AddDbContextCheck<IdentityServerPersistedGrantDbContext>("PersistedGrantsDbContext")
	.AddDbContextCheck<AdminIdentityDbContext>("IdentityDbContext")
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

app.UseCookiePolicy();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseHsts();
}

app.UseStaticFiles();
app.UseIdentityServer();

var options = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

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