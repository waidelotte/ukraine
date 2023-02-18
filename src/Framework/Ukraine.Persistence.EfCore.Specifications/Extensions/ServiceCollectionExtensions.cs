using Microsoft.Extensions.DependencyInjection;
using Ukraine.Persistence.EfCore.Specifications.Interfaces;
using Ukraine.Persistence.EfCore.Specifications.Repositories;

namespace Ukraine.Persistence.EfCore.Specifications.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineEfCoreSpecifications(this IServiceCollection services)
	{
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(GenericSpecificationRepository<>));
		return services;
	}
}