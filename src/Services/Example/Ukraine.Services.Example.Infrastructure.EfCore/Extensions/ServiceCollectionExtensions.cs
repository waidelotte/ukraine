using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Infrastructure.EfCore.Options;
namespace Ukraine.Services.Example.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleInfrastructureEfCore(this IServiceCollection services,
		string connectionString, ExampleDatabaseOptions options)
	{
		services.AddCustomNpgsqlContext<ExampleContext, ExampleContext>(connectionString, o =>
		{
			o.RetryOnFailureCount = options.RetryOnFailureCount;
			o.RetryOnFailureDelay = options.RetryOnFailureDelay;
		});

		services.AddCustomUnitOfWork<ExampleContext>();
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(ExampleRepository<>));
		
		return services;
	}
}