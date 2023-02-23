using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Interceptors;

namespace Ukraine.EfCore.Extensions;

public static class DbContextOptionsBuilderExtensions
{
	public static DbContextOptionsBuilder AddUkraineAudit(this DbContextOptionsBuilder builder)
	{
		return builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
	}

	public static DbContextOptionsBuilder UseUkraineNamingConvention(this DbContextOptionsBuilder builder)
	{
		return builder.UseSnakeCaseNamingConvention();
	}

	public static DbContextOptionsBuilder UseUkrainePostgres<TMigrationAssembly>(
		this DbContextOptionsBuilder builder,
		string connectionString,
		string? migrationsSchema = null)
	{
		return builder.UseNpgsql(connectionString, sqlOptions =>
		{
			sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);
			sqlOptions.MigrationsHistoryTable("__migrations", migrationsSchema);
		});
	}
}