using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.EfCore.Contexts;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EfCore.Options;
using Ukraine.Infrastructure.EfCore.Repositories;

namespace Ukraine.Infrastructure.EfCore.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomNpgsqlContext<TContext, TMigrationAssembly>(
		this IServiceCollection services,
		string connectionString,
		Action<CustomNpgsqlOptions> options) where TContext : DbContext, IDatabaseFacadeResolver
	{
		var opt = new CustomNpgsqlOptions();
		options.Invoke(opt);
		
		services.AddDbContextPool<TContext>(o =>
		{
			o.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);
				sqlOptions.EnableRetryOnFailure(opt.RetryOnFailureCount, opt.RetryOnFailureDelay, null);
			}).UseSnakeCaseNamingConvention();
		});

		services.AddScoped<IDatabaseFacadeResolver>(provider => provider.GetRequiredService<TContext>());
		
		return services;
	}
	
	public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services) 
		where TDbContext : DbContext
	{
		services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();
		services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
		
		return services;
	}
}