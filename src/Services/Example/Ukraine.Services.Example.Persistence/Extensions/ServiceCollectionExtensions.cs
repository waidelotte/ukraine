using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.EfCore.Extensions;
using Ukraine.HealthChecks.Extenstion;
using Ukraine.Services.Example.Domain.Exceptions;

namespace Ukraine.Services.Example.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Postgres");

		if (string.IsNullOrEmpty(connectionString))
			throw ExampleException.Exception("Postgres Connection String is null or empty");

		services.AddUkrainePostgresContext<ExampleContext, ExampleContext>(connectionString, configuration.GetSection("UkrainePostgres"));
		services.AddUkraineSpecificationRepositories();
		services.AddUkraineUnitOfWork();
		services.AddUkraineHealthChecks().AddUkrainePostgresHealthCheck(connectionString);

		return services;
	}
}