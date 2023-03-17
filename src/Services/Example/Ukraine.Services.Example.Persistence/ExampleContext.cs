using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Persistence;

public sealed class ExampleContext : DbContext, IDatabaseFacadeResolver
{
	private readonly IDateTimeProvider _dateTimeProvider;

	public ExampleContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
	{
		_dateTimeProvider = dateTimeProvider;
	}

	public DbSet<Author> Authors => Set<Author>();

	public DbSet<Book> Books => Set<Book>();

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedUtc = _dateTimeProvider.NowUtc;
					break;

				case EntityState.Modified:
					entry.Entity.LastModifiedUtc = _dateTimeProvider.NowUtc;
					break;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema("ukraine_example");
		modelBuilder.HasPostgresUuidGenerator();
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			entityType.IgnoreDomainEventProperty(modelBuilder);
			entityType.ApplyUtcToDateTime();
			entityType.ApplyUuid(modelBuilder);
			entityType.ApplyDefaultsToAuditable(modelBuilder);
		}
	}
}