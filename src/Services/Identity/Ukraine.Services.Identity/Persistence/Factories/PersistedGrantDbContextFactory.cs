using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ukraine.Services.Identity.Persistence.Factories;

internal sealed class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
	public PersistedGrantDbContext CreateDbContext(string[] args)
	{
		var connectionString = "Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;";

		var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();

		optionsBuilder.UseSnakeCaseNamingConvention();
		optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
		{
			sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
			sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_operational");
		});

		IServiceCollection services = new ServiceCollection();

		services.AddIdentityServer()
			.AddOperationalStore(options =>
			{
				options.DefaultSchema = "ukraine_identity_operational";
				options.ConfigureDbContext = b =>
				{
					b.UseUpperSnakeCaseNamingConvention();
					b.UseNpgsql(connectionString, sqlOptions =>
						{
							sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
							sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity_operational");
						});
				};
			});

		optionsBuilder.UseApplicationServiceProvider(services.BuildServiceProvider());

		return new PersistedGrantDbContext(optionsBuilder.Options);
	}
}