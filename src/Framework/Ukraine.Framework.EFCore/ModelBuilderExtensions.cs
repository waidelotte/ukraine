using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public static class ModelBuilderExtensions
{
	public static ModelBuilder HasPostgresUuidGenerator(this ModelBuilder modelBuilder)
		=> modelBuilder.HasPostgresExtension("uuid-ossp");
}