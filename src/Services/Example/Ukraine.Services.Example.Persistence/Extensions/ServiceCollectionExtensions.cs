using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Persistence.EfCore.Extensions;
using Ukraine.Persistence.EfCore.Specifications.Extensions;
using Ukraine.Services.Example.Persistence.Options;

namespace Ukraine.Services.Example.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		string connectionString,
		IConfiguration configuration)
	{
		var databaseOptions = configuration.GetRequiredSection<ExampleDatabaseOptions>(ExampleDatabaseOptions.SECTION_NAME);

		services
			.AddUkrainePostgresContext<ExampleContext, ExampleContext>(connectionString, options =>
			{
				options.RetryOnFailureDelay = databaseOptions.RetryOnFailureDelay;
				options.RetryOnFailureCount = databaseOptions.RetryOnFailureCount;
				options.DetailedErrors = databaseOptions.DetailedErrors;
				options.SensitiveDataLogging = databaseOptions.SensitiveDataLogging;
			})
			.AddUkraineEfCoreSpecifications()
			.AddUkraineUnitOfWork();

		return services;
	}
}