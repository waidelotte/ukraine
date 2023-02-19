using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Interfaces;
using Ukraine.Persistence.EfCore.Interceptors;
using Ukraine.Persistence.EfCore.Interfaces;
using Ukraine.Persistence.EfCore.Options;
using Ukraine.Persistence.EfCore.Repositories;

namespace Ukraine.Persistence.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection UseUkraineInMemoryDatabase<TContext>(
		this IServiceCollection services, Action<UkraineDatabaseOptions>? configure)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		var options = new UkrainePostgresOptions();
		configure?.Invoke(options);

		return ConfigureContext<TContext>(services, options, dbBuilder =>
		{
			dbBuilder.UseInMemoryDatabase(Constants.IN_MEMORY_DATABASE_NAME);
		});
	}

	public static IServiceCollection AddUkrainePostgresContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString,
		Action<UkrainePostgresOptions>? configure)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		var options = new UkrainePostgresOptions();
		configure?.Invoke(options);

		return ConfigureContext<TContext>(services, options, dbBuilder =>
		{
			dbBuilder.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);

				if (options.RetryOnFailureCount.HasValue && options.RetryOnFailureDelay.HasValue)
				{
					sqlOptions.EnableRetryOnFailure(
						options.RetryOnFailureCount.Value,
						options.RetryOnFailureDelay.Value,
						null);
				}
			});
		});
	}

	public static IServiceCollection AddUkraineUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
		return services;
	}

	private static IServiceCollection ConfigureContext<TContext>(
		IServiceCollection services,
		UkraineDatabaseOptions options,
		Action<DbContextOptionsBuilder> configureContext)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		services.AddDbContextPool<TContext>(dbBuilder =>
		{
			dbBuilder.EnableDetailedErrors(options.DetailedErrors);
			dbBuilder.EnableSensitiveDataLogging(options.SensitiveDataLogging);

			if (options.UseAuditSave)
				dbBuilder.AddInterceptors(new AuditEntitiesSaveInterceptor());

			dbBuilder.UseSnakeCaseNamingConvention();

			configureContext.Invoke(dbBuilder);
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		services.AddScoped<DbContext, TContext>();

		return services;
	}
}