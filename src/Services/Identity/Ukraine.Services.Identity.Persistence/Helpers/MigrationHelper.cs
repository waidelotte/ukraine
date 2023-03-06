using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukraine.Services.Identity.Persistence.Configuration;
using Ukraine.Services.Identity.Persistence.DbContexts;
using Ukraine.Services.Identity.Persistence.Entities;

namespace Ukraine.Services.Identity.Persistence.Helpers;

public static class MigrationHelper
{
	public static async Task ApplyMigrationsWithDataSeedAsync(this IHost host)
	{
		using var serviceScope = host.Services.CreateScope();

		var services = serviceScope.ServiceProvider;

		await MigrateDatabaseAsync(services);

		await SeedDataAsync(services);
	}

	public static async Task MigrateDatabaseAsync(IServiceProvider services)
	{
		using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

		await using (var context = scope.ServiceProvider.GetRequiredService<IdentityServerPersistedGrantDbContext>())
		{
			await context.Database.MigrateAsync();
		}

		await using (var context = scope.ServiceProvider.GetRequiredService<ServiceIdentityDbContext>())
		{
			await context.Database.MigrateAsync();
		}

		await using (var context = scope.ServiceProvider.GetRequiredService<IdentityServerConfigurationDbContext>())
		{
			await context.Database.MigrateAsync();
		}

		await using (var context = scope.ServiceProvider.GetRequiredService<IdentityServerDataProtectionDbContext>())
		{
			await context.Database.MigrateAsync();
		}
	}

	public static async Task SeedDataAsync(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<IdentityServerConfigurationDbContext>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();
		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserIdentityRole>>();

		var identityServerData = scope.ServiceProvider.GetRequiredService<IdentityServerData>();
		var identityData = scope.ServiceProvider.GetRequiredService<IdentityData>();

		await SeedIdentityServerData(context, identityServerData);
		await SeedIdentityData(userManager, roleManager, identityData);
	}

	private static async Task SeedIdentityData(
		UserManager<UserIdentity> userManager,
		RoleManager<UserIdentityRole> roleManager,
		IdentityData identityData)
	{
		foreach (var r in identityData.Roles)
		{
			if (!await roleManager.RoleExistsAsync(r.Name))
			{
				var role = new UserIdentityRole
				{
					Name = r.Name
				};

				var result = await roleManager.CreateAsync(role);

				if (result.Succeeded)
				{
					foreach (var claim in r.Claims)
						await roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
				}
			}
		}

		foreach (var user in identityData.Users)
		{
			var identityUser = new UserIdentity
			{
				UserName = user.Username,
				Email = user.Email,
				EmailConfirmed = true
			};

			var userByUserName = await userManager.FindByNameAsync(user.Username);
			var userByEmail = await userManager.FindByEmailAsync(user.Email);

			if (userByUserName != default || userByEmail != default) continue;

			var result = await userManager.CreateAsync(identityUser, user.Password);

			if (result.Succeeded)
			{
				foreach (var claim in user.Claims)
					await userManager.AddClaimAsync(identityUser, new Claim(claim.Type, claim.Value));

				foreach (var role in user.Roles) await userManager.AddToRoleAsync(identityUser, role);
			}
		}
	}

	private static async Task SeedIdentityServerData(
		IdentityServerConfigurationDbContext context,
		IdentityServerData identityServerDataConfiguration)
	{
		foreach (var resource in identityServerDataConfiguration.IdentityResources)
		{
			var exits = await context.IdentityResources.AnyAsync(a => a.Name == resource.Name);

			if (exits) continue;

			await context.IdentityResources.AddAsync(resource.ToEntity());
		}

		foreach (var apiScope in identityServerDataConfiguration.ApiScopes)
		{
			var exits = await context.ApiScopes.AnyAsync(a => a.Name == apiScope.Name);

			if (exits) continue;

			await context.ApiScopes.AddAsync(apiScope.ToEntity());
		}

		foreach (var resource in identityServerDataConfiguration.ApiResources)
		{
			var exits = await context.ApiResources.AnyAsync(a => a.Name == resource.Name);

			if (exits) continue;

			foreach (var s in resource.ApiSecrets) s.Value = s.Value.ToSha256();

			await context.ApiResources.AddAsync(resource.ToEntity());
		}

		foreach (var client in identityServerDataConfiguration.Clients)
		{
			var exits = await context.Clients.AnyAsync(a => a.ClientId == client.ClientId);

			if (exits) continue;

			foreach (var secret in client.ClientSecrets) secret.Value = secret.Value.ToSha256();

			client.Claims = client.ClientClaims
				.Select(c => new ClientClaim(c.Type, c.Value))
				.ToList();

			await context.Clients.AddAsync(client.ToEntity());
		}

		await context.SaveChangesAsync();
	}
}