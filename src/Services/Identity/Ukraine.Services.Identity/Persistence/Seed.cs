using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ukraine.Services.Identity.Exceptions;
using Ukraine.Services.Identity.Models;

namespace Ukraine.Services.Identity.Persistence;

public static class Seed
{
	public static async Task SeedAdminRoleAsync(this IServiceScope scope)
	{
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		if (await roleManager.FindByNameAsync("admin") == null)
		{
			await roleManager.CreateAsync(new IdentityRole("admin"));
		}
	}

	public static async Task SeedDevUserAsync(this IServiceScope scope)
	{
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UkraineUser>>();

		var devUser = await userManager.FindByNameAsync("dev");

		if (devUser == null)
		{
			devUser = new UkraineUser
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "dev",
				Email = "dev@email.com",
				EmailConfirmed = true,
				PhoneNumber = "1234567890",
				PhoneNumberConfirmed = true
			};

			var result = await userManager.CreateAsync(devUser, "devdev");

			if (!result.Succeeded)
			{
				throw IdentityException.Exception(result.Errors.First().Description);
			}

			await userManager.AddToRoleAsync(devUser, "admin");
		}
	}

	public static async Task SeedApiResourcesAsync(this IServiceScope scope)
	{
		var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

		if (!await configurationDbContext.ApiResources.AnyAsync())
		{
			await configurationDbContext.ApiResources.AddAsync(new ApiResource
			{
				Name = "service-example-api",
				DisplayName = "Example API",
				Scopes = new List<string>
				{
					"API_SWAGGER_EXAMPLE_SCOPE",
					"API_GRAPHQL_EXAMPLE_SCOPE"
				}
			}.ToEntity());

			await configurationDbContext.SaveChangesAsync();
		}
	}

	public static async Task SeedApiScopesAsync(this IServiceScope scope)
	{
		var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

		if (!await configurationDbContext.ApiScopes.AnyAsync())
		{
			await configurationDbContext.ApiScopes.AddRangeAsync(
				new ApiScope
				{
					Name = "API_SWAGGER_EXAMPLE_SCOPE",
					DisplayName = "Access to Example API from Swagger"
				}.ToEntity(),
				new ApiScope
				{
					Name = "API_GRAPHQL_EXAMPLE_SCOPE",
					DisplayName = "Access to Example API from GraphQl"
				}.ToEntity());

			await configurationDbContext.SaveChangesAsync();
		}
	}

	public static async Task SeedIdentityResourcesAsync(this IServiceScope scope)
	{
		var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

		if (!await configurationDbContext.IdentityResources.AnyAsync())
		{
			await configurationDbContext.IdentityResources.AddRangeAsync(
				new IdentityResources.OpenId().ToEntity());

			await configurationDbContext.SaveChangesAsync();
		}
	}

	public static async Task SeedClientsAsync(this IServiceScope scope)
	{
		var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

		if (!await configurationDbContext.Clients.AnyAsync())
		{
			await configurationDbContext.Clients.AddRangeAsync(
				new Client
				{
					ClientId = "service-example-api-swagger",
					ClientName = "Example API Swagger",
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowAccessTokensViaBrowser = true,
					AllowedScopes = new List<string>
					{
						"API_SWAGGER_EXAMPLE_SCOPE"
					},
					PostLogoutRedirectUris = new List<string>
					{
						"http://localhost:7003/swagger/"
					},
					RedirectUris = new List<string>
					{
						"http://localhost:7003/swagger/oauth2-redirect.html"
					}
				}.ToEntity(),
				new Client
				{
					ClientId = "service-example-api-banana",
					ClientSecrets = new List<Secret> { new("localhostSecret".Sha512()) },
					ClientName = "Example API Banana",
					AllowedGrantTypes = GrantTypes.Code,
					AllowedScopes = new List<string>
					{
						"openid",
						"API_GRAPHQL_EXAMPLE_SCOPE"
					},
					AllowedCorsOrigins = new List<string>
					{
						"http://localhost:7003"
					},
					PostLogoutRedirectUris = new List<string>
					{
						"http://localhost:7003/graphql"
					},
					RedirectUris = new List<string>
					{
						"http://localhost:7003/graphql/#/oauth"
					}
				}.ToEntity());

			await configurationDbContext.SaveChangesAsync();
		}
	}
}