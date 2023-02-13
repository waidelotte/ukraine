using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Options;
using Ukraine.Infrastructure.Specifications.EfCore.Extensions;

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