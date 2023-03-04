using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using IdentityModel;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoruba.AuditLogging.EntityFramework.DbContexts;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Configuration.Configuration;
using Skoruba.Duende.IdentityServer.Admin.EntityFramework.Interfaces;

namespace Ukraine.Services.Identity.Persistence.Helpers;

public static class DbMigrationHelpers
{
	public static async Task<bool> ApplyDbMigrationsWithDataSeedAsync<TIdentityServerDbContext, TIdentityDbContext,
		TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext, TDataProtectionDbContext, TUser, TRole>(
		IHost host,
		SeedConfiguration? seedConfiguration,
		DatabaseMigrationsConfiguration? databaseMigrationsConfiguration)
		where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
		where TIdentityDbContext : DbContext
		where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
		where TLogDbContext : DbContext, IAdminLogDbContext
		where TAuditLogDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
		where TDataProtectionDbContext : DbContext, IDataProtectionKeyContext
		where TUser : IdentityUser, new()
		where TRole : IdentityRole, new()
	{
		var migrationComplete = false;

		using (var serviceScope = host.Services.CreateScope())
		{
			var services = serviceScope.ServiceProvider;

			if (databaseMigrationsConfiguration is { ApplyDatabaseMigrations: true })
			{
				migrationComplete =
					await EnsureDatabasesMigratedAsync<TIdentityDbContext, TIdentityServerDbContext,
						TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext,
						TDataProtectionDbContext>(services);
			}

			if (seedConfiguration is { ApplySeed: true })
			{
				var seedComplete = await EnsureSeedDataAsync<TIdentityServerDbContext, TUser, TRole>(services);

				return migrationComplete && seedComplete;
			}
		}

		return migrationComplete;
	}

	public static async Task<bool> EnsureDatabasesMigratedAsync<TIdentityDbContext, TConfigurationDbContext,
		TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext, TDataProtectionDbContext>(
		IServiceProvider services)
		where TIdentityDbContext : DbContext
		where TPersistedGrantDbContext : DbContext
		where TConfigurationDbContext : DbContext
		where TLogDbContext : DbContext
		where TAuditLogDbContext : DbContext
		where TDataProtectionDbContext : DbContext
	{
		var pendingMigrationCount = 0;

		using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			using (var context = scope.ServiceProvider.GetRequiredService<TPersistedGrantDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TIdentityDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TConfigurationDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TLogDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TAuditLogDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TDataProtectionDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}
		}

		return pendingMigrationCount == 0;
	}

	public static async Task<bool> EnsureSeedDataAsync<TIdentityServerDbContext, TUser, TRole>(
		IServiceProvider serviceProvider)
		where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
		where TUser : IdentityUser, new()
		where TRole : IdentityRole, new()
	{
		using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var context = scope.ServiceProvider.GetRequiredService<TIdentityServerDbContext>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<TRole>>();
			var idsDataConfiguration = scope.ServiceProvider.GetRequiredService<IdentityServerData>();
			var idDataConfiguration = scope.ServiceProvider.GetRequiredService<IdentityData>();

			await EnsureSeedIdentityServerData(context, idsDataConfiguration);
			await EnsureSeedIdentityData(userManager, roleManager, idDataConfiguration);
		}

		return true;
	}

	private static async Task EnsureSeedIdentityData<TUser, TRole>(
		UserManager<TUser> userManager,
		RoleManager<TRole> roleManager,
		IdentityData identityDataConfiguration)
		where TUser : IdentityUser, new()
		where TRole : IdentityRole, new()
	{
		foreach (var r in identityDataConfiguration.Roles)
		{
			if (!await roleManager.RoleExistsAsync(r.Name))
			{
				var role = new TRole
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

		foreach (var user in identityDataConfiguration.Users)
		{
			var identityUser = new TUser
			{
				UserName = user.Username,
				Email = user.Email,
				EmailConfirmed = true
			};

			var userByUserName = await userManager.FindByNameAsync(user.Username);
			var userByEmail = await userManager.FindByEmailAsync(user.Email);

			if (userByUserName != default || userByEmail != default) continue;

			// if there is no password we create user without password
			// user can reset password later, because accounts have EmailConfirmed set to true
			var result = !string.IsNullOrEmpty(user.Password)
				? await userManager.CreateAsync(identityUser, user.Password)
				: await userManager.CreateAsync(identityUser);

			if (result.Succeeded)
			{
				foreach (var claim in user.Claims)
					await userManager.AddClaimAsync(identityUser, new Claim(claim.Type, claim.Value));

				foreach (var role in user.Roles) await userManager.AddToRoleAsync(identityUser, role);
			}
		}
	}

	private static async Task EnsureSeedIdentityServerData<TIdentityServerDbContext>(
		TIdentityServerDbContext context,
		IdentityServerData identityServerDataConfiguration)
		where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
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