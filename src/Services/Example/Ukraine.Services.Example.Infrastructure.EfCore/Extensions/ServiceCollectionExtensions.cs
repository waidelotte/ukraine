using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Options;
using Ukraine.Infrastructure.EfCore.Specifications.Extensions;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructureEfCore(
		this IServiceCollection services,
		string connectionString,
		Action<UkrainePostgresOptions>? options = null)
	{
		services
			.AddUkrainePostgresContext<ExampleContext, ExampleContext>(connectionString, options)
			.AddUkraineEfCoreSpecifications()
			.AddUkraineUnitOfWork();

		return services;
	}
}