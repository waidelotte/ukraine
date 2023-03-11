using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Framework.Core.HealthChecks;

public static class HealthChecksBuilderExtensions
{
	public static IHealthChecksBuilder AddDefaultCheck(
		this IHealthChecksBuilder builder,
		string name,
		IEnumerable<string> tags)
	{
		return builder.AddCheck(name, () => HealthCheckResult.Healthy(), tags);
	}
}