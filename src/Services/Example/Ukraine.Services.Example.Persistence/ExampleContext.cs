using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Contexts;
using Ukraine.EfCore.Extensions;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence;

public class ExampleContext : UkraineDatabaseContextBase
{
	public ExampleContext(DbContextOptions options) : base(options) { }

	public DbSet<Author> Authors => Set<Author>();

	public DbSet<Book> Books => Set<Book>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		optionsBuilder.AddUkraineAudit();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema("ukraine_example");
		modelBuilder.HasUkrainePostgresUuidGenerator();
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}