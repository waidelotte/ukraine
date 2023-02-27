using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGenericRepository(this IServiceCollection serviceCollection)
	{
		serviceCollection.TryAddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
		return serviceCollection;
	}

	public static IServiceCollection AddSpecificationRepository(this IServiceCollection serviceCollection)
	{
		serviceCollection.TryAddScoped(typeof(ISpecificationRepository<>), typeof(GenericSpecificationRepository<>));
		return serviceCollection;
	}

	public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
	{
		serviceCollection.TryAddScoped<IUnitOfWork, UnitOfWork>();
		return serviceCollection;
	}

	public static IServiceCollection AddDatabaseFacadeResolver<TContext>(this IServiceCollection serviceCollection)
		where TContext : IDatabaseFacadeResolver
	{
		serviceCollection.TryAddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		return serviceCollection;
	}
}