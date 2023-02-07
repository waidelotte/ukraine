using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.Infrastructure.EventBus.Dapr;

internal sealed class HealthCheck : IHealthCheck
{
	private readonly DaprClient _daprClient;

	public HealthCheck(DaprClient daprClient)
	{
		_daprClient = daprClient;
	}

	public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
	{
		var healthy = await _daprClient.CheckHealthAsync(cancellationToken);
            
		return healthy ? HealthCheckResult.Healthy() : new HealthCheckResult(context.Registration.FailureStatus);
	}
}