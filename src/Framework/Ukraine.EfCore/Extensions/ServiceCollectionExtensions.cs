using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Domain.Interfaces;
using Ukraine.EfCore.Interfaces;
using Ukraine.EfCore.Options;
using Ukraine.EfCore.Repositories;

namespace Ukraine.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkrainePostgresContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString,
		IConfigurationSection configurationSection)
		where TContext : DbContext, IDatabaseFacadeResolver
	{
		services.AddOptions<UkrainePostgresOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkrainePostgresOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		services.AddDbContext<TContext>(dbBuilder =>
		{
			dbBuilder.EnableDetailedErrors(options.EnableDetailedErrors);
			dbBuilder.EnableSensitiveDataLogging(options.EnableSensitiveDataLogging);
			dbBuilder.UseSnakeCaseNamingConvention();

			dbBuilder.UseUkrainePostgres<TMigrationAssembly>(connectionString, o =>
			{
				o.SetOptions(options);
			});
		});

		services.TryAddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		services.TryAddScoped<DbContext, TContext>();

		return services;
	}

	public static IServiceCollection AddUkraineRepositories(this IServiceCollection services)
	{
		services.TryAddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
		return services;
	}

	public static IServiceCollection AddUkraineSpecificationRepositories(this IServiceCollection services)
	{
		services.TryAddScoped(typeof(ISpecificationRepository<>), typeof(GenericSpecificationRepository<>));
		return services;
	}

	public static IServiceCollection AddUkraineUnitOfWork(this IServiceCollection services)
	{
		AddUkraineRepositories(services);
		services.TryAddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}