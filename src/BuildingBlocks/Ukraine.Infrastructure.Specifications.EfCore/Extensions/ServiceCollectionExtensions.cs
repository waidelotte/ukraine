using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Specifications.EfCore.Interfaces;
using Ukraine.Infrastructure.Specifications.EfCore.Repositories;

namespace Ukraine.Infrastructure.Specifications.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineEfCoreSpecifications(this IServiceCollection services)
	{
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(GenericSpecificationRepository<>));
		return services;
	}
}