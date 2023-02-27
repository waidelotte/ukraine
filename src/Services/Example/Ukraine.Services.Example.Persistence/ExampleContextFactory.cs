using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ukraine.Services.Example.Persistence;

internal sealed class ExampleContextFactory : IDesignTimeDbContextFactory<ExampleContext>
{
	public ExampleContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();

		optionsBuilder.UseSnakeCaseNamingConvention();
		optionsBuilder.UseNpgsql(
			"Server=localhost;Port=5432;Database=ukraine;User Id=postgres;Password=postgres;", sqlOptions =>
			{
				sqlOptions.MigrationsAssembly(typeof(ExampleContext).Assembly.GetName().Name);
				sqlOptions.MigrationsHistoryTable("__migrations", "ukraine_example");
			});

		return new ExampleContext(optionsBuilder.Options);
	}
}