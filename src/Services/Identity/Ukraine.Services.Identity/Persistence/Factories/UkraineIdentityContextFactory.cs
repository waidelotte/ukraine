using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ukraine.EfCore.Extensions;

namespace Ukraine.Services.Identity.Persistence.Factories;

public class UkraineIdentityContextFactory : IDesignTimeDbContextFactory<UkraineIdentityContext>
{
	public UkraineIdentityContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<UkraineIdentityContext>();

		optionsBuilder.UseUkraineNamingConvention();
		optionsBuilder
			.UseUkrainePostgres<UkraineIdentityContextFactory>(
				"Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;",
				"ukraine_identity");

		return new UkraineIdentityContext(optionsBuilder.Options);
	}
}