using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Specifications.Interfaces;
using Ukraine.Infrastructure.EfCore.Specifications.Repositories;

namespace Ukraine.Infrastructure.EfCore.Specifications.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineSpecifications(this IServiceCollection services)
	{
		services.AddScoped(typeof(ISpecificationRepository<>), typeof(SpecificationRepository<>));
		return services;
	}
}