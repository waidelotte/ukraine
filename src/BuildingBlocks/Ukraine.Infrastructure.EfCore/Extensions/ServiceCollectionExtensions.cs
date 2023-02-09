using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EfCore.Options;
using Ukraine.Infrastructure.EfCore.Repositories;

namespace Ukraine.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkrainePostgresContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString,
		Action<UkrainePostgresOptions>? options) where TContext : DbContext, IDatabaseFacadeResolver
	{
		var opt = new UkrainePostgresOptions();
		options?.Invoke(opt);
		
		services.AddDbContextPool<TContext>(o =>
		{
			o.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);
				
				if(opt.RetryOnFailureCount.HasValue && opt.RetryOnFailureDelay.HasValue)
					sqlOptions.EnableRetryOnFailure(opt.RetryOnFailureCount.Value, opt.RetryOnFailureDelay.Value, null);
				
			}).UseSnakeCaseNamingConvention();
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		
		return services;
	}
	
	public static IServiceCollection AddUkraineUnitOfWork<TDbContext>(this IServiceCollection services) 
		where TDbContext : DbContext
	{
		services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();
		return services;
	}
}