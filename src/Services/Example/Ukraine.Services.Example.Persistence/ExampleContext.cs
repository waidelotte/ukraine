using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ukraine.Persistence.EfCore.Contexts;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence;

public class ExampleContext : UkraineDatabaseContextBase
{
	public ExampleContext(DbContextOptions options) : base(options) { }

	public DbSet<Author> Authors => Set<Author>();

	public DbSet<Book> Books => Set<Book>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(Constants.SCHEMA_NAME);
		modelBuilder.HasPostgresExtension(Ukraine.Persistence.EfCore.Constants.Extensions.UUID_GENERATOR);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}