using Microsoft.EntityFrameworkCore;

namespace Ukraine.Framework.EFCore;

public static class ModelBuilderExtensions
{
	public static ModelBuilder HasPostgresUuidGenerator(this ModelBuilder modelBuilder)
		=> modelBuilder.HasPostgresExtension("uuid-ossp");

	public static ModelBuilder ApplyToUtcDateTimeConverter(this ModelBuilder builder)
	{
		foreach (var entityType in builder.Model.GetEntityTypes())
		{
			foreach (var property in entityType.GetProperties())
			{
				if (property.ClrType == typeof(DateTime) ||
					property.ClrType == typeof(DateTime?))
				{
					property.SetValueConverter(DateTimeConverter.ToUtcConverter);
				}
			}
		}

		return builder;
	}
}