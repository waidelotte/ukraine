using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Framework.EFCore;

public static class ServiceProviderExtensions
{
	public static async Task MigrateDatabaseAsync<TContext>(this IServiceProvider serviceProvider)
		where TContext : DbContext
	{
		using var scope = serviceProvider.CreateScope();
		await MigrateDatabaseAsync<TContext>(scope);
	}

	public static async Task MigrateDatabaseAsync<TContext>(this IServiceScope scope)
		where TContext : DbContext
	{
		var context = scope.ServiceProvider.GetRequiredService<TContext>();
		await context.Database.MigrateAsync();
	}
}