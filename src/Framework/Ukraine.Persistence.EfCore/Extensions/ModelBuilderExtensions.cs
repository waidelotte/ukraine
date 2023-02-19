using Microsoft.EntityFrameworkCore;

namespace Ukraine.Persistence.EfCore.Extensions;

public static class ModelBuilderExtensions
{
	public static ModelBuilder HasPostgresUuidGenerator(this ModelBuilder modelBuilder)
	{
		return modelBuilder.HasPostgresExtension(Constants.Extensions.UUID_GENERATOR);
	}
}