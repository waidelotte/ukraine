using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ukraine.EfCore.Extensions;

namespace Ukraine.Services.Identity.Persistence.Factories;

public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
	public PersistedGrantDbContext CreateDbContext(string[] args)
	{
		var connectionString = "Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;";

		var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();

		optionsBuilder.UseUkraineNamingConvention();
		optionsBuilder.UseUkrainePostgres<Program>(connectionString, "ukraine_identity_operational");

		IServiceCollection services = new ServiceCollection();

		services.AddIdentityServer()
			.AddOperationalStore(options =>
			{
				options.DefaultSchema = "ukraine_identity_operational";
				options.ConfigureDbContext = b =>
				{
					b.UseUkraineNamingConvention();
					b.UseUkrainePostgres<Program>(connectionString, "ukraine_identity_operational");
				};
			});

		optionsBuilder.UseApplicationServiceProvider(services.BuildServiceProvider());

		return new PersistedGrantDbContext(optionsBuilder.Options);
	}
}