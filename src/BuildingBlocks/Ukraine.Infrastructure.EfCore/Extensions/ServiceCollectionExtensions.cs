using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomNpgsqlContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		return AddCustomNpgsqlContext<TContext, TMigrationAssembly>(services, connectionString, 5, TimeSpan.FromSeconds(10));
	}

	public static IServiceCollection AddCustomNpgsqlContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString, int maxRetryOnFailCount, TimeSpan retryOnFailDelay)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		services.AddPooledDbContextFactory<TContext>(options =>
		{
			options.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);
				sqlOptions.EnableRetryOnFailure(maxRetryOnFailCount, retryOnFailDelay, null);
			}).UseSnakeCaseNamingConvention();
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetService<IDbContextFactory<TContext>>()!.CreateDbContext());
		
		return services;
	}
}