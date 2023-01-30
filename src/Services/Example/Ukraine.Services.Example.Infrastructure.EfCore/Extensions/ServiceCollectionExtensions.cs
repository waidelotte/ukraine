using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Extensions;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleInfrastructureEfCore(this IServiceCollection services, string connectionString)
	{
		services.AddCustomNpgsqlContext<ExampleContext, ExampleContext>(connectionString);
		
		return services;
	}
}