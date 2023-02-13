using Microsoft.Extensions.DependencyInjection;
using Ukraine.Persistence.EfCore.Extensions;
using Ukraine.Persistence.EfCore.Options;
using Ukraine.Persistence.Specifications.EfCore.Extensions;

namespace Ukraine.Services.Example.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		string connectionString,
		Action<UkrainePostgresOptions>? options = null)
	{
		services
			.AddUkrainePostgresContext<ExampleContext, ExampleContext>(connectionString, options)
			.AddUkraineEfCoreSpecifications()
			.AddUkraineEfUnitOfWork();

		return services;
	}
}