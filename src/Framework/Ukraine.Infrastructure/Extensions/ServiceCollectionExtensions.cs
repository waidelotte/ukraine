using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IMvcBuilder AddUkraineControllers(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddControllers();
	}
}