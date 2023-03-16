using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence;

public sealed class ExampleContext : DbContext, IDatabaseFacadeResolver
{
	public ExampleContext(DbContextOptions options) : base(options) { }

	public DbSet<Author> Authors => Set<Author>();

	public DbSet<Book> Books => Set<Book>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema("ukraine_example");
		modelBuilder.HasPostgresUuidGenerator();
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			if (typeof(IEntity<Guid>).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder
					.Entity(entityType.ClrType)
					.Property(nameof(IEntity<Guid>.Id))
					.HasUuidColumnType()
					.HasDefaultPostgresUuid();

				modelBuilder
					.Entity(entityType.ClrType)
					.Property(nameof(IEntity<Guid>.Created))
					.HasDefaultPostgresDate();
			}
		}

		modelBuilder.ApplyToUtcDateTimeConverter();
	}
}