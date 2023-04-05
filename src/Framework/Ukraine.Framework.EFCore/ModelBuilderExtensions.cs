using Microsoft.EntityFrameworkCore;

namespace Ukraine.Framework.EFCore;

public static class ModelBuilderExtensions
{
	public static ModelBuilder HasPostgresUuidGenerator(this ModelBuilder modelBuilder)
		=> modelBuilder.HasPostgresExtension("uuid-ossp");
}