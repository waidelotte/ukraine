using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Infrastructure.EventBus;

internal sealed class DaprHealthCheck : IHealthCheck
{
	private readonly DaprClient _daprClient;

	public DaprHealthCheck(DaprClient daprClient)
	{
		_daprClient = daprClient;
	}

	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		var healthy = await _daprClient.CheckHealthAsync(cancellationToken);

		return healthy ? HealthCheckResult.Healthy() : new HealthCheckResult(context.Registration.FailureStatus);
	}
}