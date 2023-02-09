using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EfCore.Options;
namespace Ukraine.Services.Example.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleInfrastructureEfCore(this IServiceCollection services,
		string connectionString, Action<UkrainePostgresOptions>? options = null)
	{
		services.AddUkrainePostgresContext<ExampleContext, ExampleContext>(connectionString, options);
		services.AddUkraineUnitOfWork<ExampleContext>();
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(ExampleRepository<>));
		
		return services;
	}
}