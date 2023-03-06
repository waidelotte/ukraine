using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ukraine.Framework.Core.HealthChecks
{
	public static class WebApplicationExtensions
	{
		public static void MapDefaultHealthChecks(this WebApplication webApplication)
		{
			webApplication.MapHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
		}
	}
}