using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Ukraine.EfCore.Extensions;

namespace Ukraine.Services.Example.Persistence;

public class ExampleContextFactory : IDesignTimeDbContextFactory<ExampleContext>
{
	public ExampleContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();

		optionsBuilder.UseUkraineNamingConvention();
		optionsBuilder
			.UseUkrainePostgres<ExampleContextFactory>(
				"Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;",
				"ukraine_example");

		return new ExampleContext(optionsBuilder.Options);
	}
}