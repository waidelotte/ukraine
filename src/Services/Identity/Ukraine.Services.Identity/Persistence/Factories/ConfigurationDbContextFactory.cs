using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ukraine.Services.Identity.Persistence.Factories;

internal sealed class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
{
	public ConfigurationDbContext CreateDbContext(string[] args)
	{
		var connectionString = "Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;";

		var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();

		optionsBuilder.UseSnakeCaseNamingConvention();
		optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
		{
			sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
			sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_configuration");
		});

		IServiceCollection services = new ServiceCollection();

		services
			.AddIdentityServer()
			.AddConfigurationStore(options =>
			{
				options.DefaultSchema = "ukraine_identity_configuration";
				options.ConfigureDbContext = b =>
				{
					b.UseSnakeCaseNamingConvention();
					b.UseNpgsql(connectionString, sqlOptions =>
					{
						sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
						sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_configuration");
					});
				};
			});

		optionsBuilder.UseApplicationServiceProvider(services.BuildServiceProvider());

		return new ConfigurationDbContext(optionsBuilder.Options);
	}
}