using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Interceptors;
using Ukraine.EfCore.Options;

namespace Ukraine.EfCore.Extensions;

public static class DbContextOptionsBuilderExtensions
{
	public static DbContextOptionsBuilder AddUkraineAudit(this DbContextOptionsBuilder builder)
	{
		return builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
	}

	public static DbContextOptionsBuilder UseUkrainePostgres<TMigrationAssembly>(
		this DbContextOptionsBuilder builder,
		string connectionString,
		Action<UkrainePostgresOptions>? configure = null)
	{
		var options = new UkrainePostgresOptions();
		configure?.Invoke(options);

		return builder.UseNpgsql(connectionString, sqlOptions =>
		{
			sqlOptions.MigrationsAssembly(typeof(TMigrationAssembly).Assembly.GetName().Name);

			if (options.RetryOnFailureCount.HasValue && options.RetryOnFailureDelay.HasValue)
			{
				sqlOptions.EnableRetryOnFailure(
					options.RetryOnFailureCount.Value,
					options.RetryOnFailureDelay.Value,
					null);
			}
		});
	}
}