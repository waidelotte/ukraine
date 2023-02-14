﻿using Microsoft.EntityFrameworkCore;
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
		this IServiceCollection services, Action<UkrainePostgresOptions>? options)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		var opt = new UkrainePostgresOptions();
		options?.Invoke(opt);

		services.AddDbContextPool<TContext>(o =>
		{
			o.ConfigureDbContextOptionsBuilder(opt);

			o.UseInMemoryDatabase(Constants.IN_MEMORY_DATABASE_NAME);
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		services.AddScoped<DbContext, TContext>();

		return services;
	}

	public static IServiceCollection AddUkrainePostgresContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString,
		Action<UkrainePostgresOptions>? options)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		var opt = new UkrainePostgresOptions();
		options?.Invoke(opt);

		services.AddDbContextPool<TContext>(o =>
		{
			o.ConfigureDbContextOptionsBuilder(opt);

			o.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);

				if (opt.RetryOnFailureCount.HasValue && opt.RetryOnFailureDelay.HasValue)
				{
					sqlOptions.EnableRetryOnFailure(opt.RetryOnFailureCount.Value, opt.RetryOnFailureDelay.Value, null);
				}
			});
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		services.AddScoped<DbContext, TContext>();

		return services;
	}

	public static IServiceCollection AddUkraineEfUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
		return services;
	}

	private static void ConfigureDbContextOptionsBuilder(
		this DbContextOptionsBuilder builder,
		UkrainePostgresOptions options)
	{
		builder.EnableDetailedErrors(options.DetailedErrors);
		builder.EnableSensitiveDataLogging(options.SensitiveDataLogging);
		builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
		builder.UseSnakeCaseNamingConvention();
	}
}