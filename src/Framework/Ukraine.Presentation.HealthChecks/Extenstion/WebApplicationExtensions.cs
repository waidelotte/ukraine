using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ukraine.Presentation.HealthChecks.Extenstion;

public static class WebApplicationExtensions
{
	public static void UseUkraineHealthChecks(this WebApplication webApplication)
	{
		webApplication.MapHealthChecks("/health/ready", new HealthCheckOptions
		{
			Predicate = reg => reg.Tags.Contains(Constants.Tags.READY),
			ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
		});
	}

	public static void UseUkraineDatabaseHealthChecks(this WebApplication webApplication)
	{
		webApplication.MapHealthChecks("/health/ready/db", new HealthCheckOptions
		{
			Predicate = r => r.Tags.Contains(Constants.Tags.DATABASE),
			ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
		});
	}
}