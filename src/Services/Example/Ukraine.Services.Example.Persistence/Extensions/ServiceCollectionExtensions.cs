using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Framework.Core;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Persistence.Options;

namespace Ukraine.Services.Example.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection serviceCollection,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetRequiredConnectionString("ukraine");

		var databaseOptions = serviceCollection
			.BindAndGetOptions<ServiceDatabaseOptions>(configuration.GetSection(ServiceDatabaseOptions.CONFIGURATION_SECTION));

		serviceCollection.AddDbContext<ExampleContext>(dbBuilder =>
		{
			dbBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
			dbBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
			dbBuilder.UseNpgsql(connectionString, sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(ExampleContext).Assembly.GetName().Name);
				sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_example");
			});

			dbBuilder.UseSnakeCaseNamingConvention();
		});

		serviceCollection.AddDatabaseFacadeResolver<ExampleContext>();
		serviceCollection.TryAddScoped<DbContext, ExampleContext>();

		serviceCollection.AddGenericRepository();
		serviceCollection.AddSpecificationRepository();
		serviceCollection.AddUnitOfWork();

		return serviceCollection;
	}
}