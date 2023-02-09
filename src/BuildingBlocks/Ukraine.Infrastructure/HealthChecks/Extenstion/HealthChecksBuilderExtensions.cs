using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Infrastructure.HealthChecks.Extenstion;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddUkrainePostgresHealthCheck(
		this IHealthChecksBuilder healthChecksBuilder,
		string connectionString)
	{
		return healthChecksBuilder
			.AddNpgSql(connectionString, name: Constants.Tags.POSTGRES, tags: new[]
			{
				Constants.Tags.READY, Constants.Tags.DATABASE, Constants.Tags.POSTGRES
			});
	}
}