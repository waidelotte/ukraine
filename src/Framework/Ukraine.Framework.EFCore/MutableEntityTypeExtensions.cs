using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public static class MutableEntityTypeExtensions
{
	public static void ApplyUtcToDateTime(this IMutableEntityType type)
	{
		foreach (var property in type.GetProperties())
		{
			if (property.ClrType == typeof(DateTime) ||
				property.ClrType == typeof(DateTime?))
			{
				property.SetValueConverter(DateTimeConverter.ToUtcConverter);
			}
		}
	}

	public static void IgnoreDomainEventProperty(this IMutableEntityType type, ModelBuilder modelBuilder)
	{
		if (typeof(IAggregateRoot).IsAssignableFrom(type.ClrType))
		{
			modelBuilder
				.Entity(type.ClrType)
				.Ignore(nameof(IAggregateRoot.DomainEvents));
		}
	}

	public static void ApplyUuid(this IMutableEntityType type, ModelBuilder modelBuilder)
	{
		if (typeof(IEntity<Guid>).IsAssignableFrom(type.ClrType))
		{
			modelBuilder
				.Entity(type.ClrType)
				.Property(nameof(IEntity<Guid>.Id))
				.HasUuidColumnType()
				.HasDefaultPostgresUuid();
		}
	}

	public static void ApplyDefaultsToAuditable(this IMutableEntityType type, ModelBuilder modelBuilder)
	{
		if (typeof(IAuditableEntity).IsAssignableFrom(type.ClrType))
		{
			modelBuilder
				.Entity(type.ClrType)
				.Property(nameof(IAuditableEntity.CreatedUtc))
				.HasDefaultPostgresDate()
				.IsRequired();
		}
	}
}