using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Framework.Dapr;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddDaprHealthCheck(
		this IHealthChecksBuilder builder,
		string name,
		HealthStatus? failureStatus = null,
		IEnumerable<string>? tags = null,
		TimeSpan? timeout = null)
	{
		return builder.AddCheck<DaprHealthCheck>(name, failureStatus, tags, timeout);
	}
}