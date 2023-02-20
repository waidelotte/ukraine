using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Persistence.EfCore.Interfaces;

namespace Ukraine.Persistence.EfCore.Extensions;

public static class ServiceProviderExtensions
{
	public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
		await context.Database.MigrateAsync();
	}
}