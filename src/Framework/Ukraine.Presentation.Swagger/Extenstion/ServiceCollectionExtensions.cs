using Microsoft.Extensions.DependencyInjection;
using Ukraine.Presentation.Swagger.Options;

namespace Ukraine.Presentation.Swagger.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineSwagger(
		this IServiceCollection serviceCollection,
		Action<UkraineSwaggerOptionsBuilder>? configure = null)
	{
		var builder = new UkraineSwaggerOptionsBuilder();
		configure?.Invoke(builder);

		return serviceCollection.AddSwaggerGen(builder.Build());
	}
}