using Microsoft.Extensions.DependencyInjection;
using Ukraine.Persistence.Specifications.EfCore.Interfaces;
using Ukraine.Persistence.Specifications.EfCore.Repositories;

namespace Ukraine.Persistence.Specifications.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineEfCoreSpecifications(this IServiceCollection services)
	{
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(GenericSpecificationRepository<>));
		return services;
	}
}