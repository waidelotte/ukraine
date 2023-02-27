using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ukraine.Services.Identity.Persistence.Factories;

internal sealed class UkraineIdentityContextFactory : IDesignTimeDbContextFactory<UkraineIdentityContext>
{
	public UkraineIdentityContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<UkraineIdentityContext>();

		optionsBuilder.UseSnakeCaseNamingConvention();
		optionsBuilder.UseNpgsql(
				"Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;", sqlOptions =>
				{
					sqlOptions.MigrationsAssembly(typeof(UkraineIdentityContext).Assembly.GetName().Name);
					sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_identity");
				});

		return new UkraineIdentityContext(optionsBuilder.Options);
	}
}