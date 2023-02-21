using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.EfCore.Interfaces;

namespace Ukraine.EfCore.Extensions;

public static class ServiceProviderExtensions
{
	public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
		await context.Database.MigrateAsync();
	}
}