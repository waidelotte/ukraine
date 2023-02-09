using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ukraine.Infrastructure.Telemetry.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineZipkinTelemetry(
		this IServiceCollection services,
		string serviceName,
		string serverUrl)
	{
		services.AddOpenTelemetryTracing(builder =>
		{
			builder
				.SetResourceBuilder(ResourceBuilder
					.CreateDefault()
					.AddService(serviceName))
				.SetSampler(new AlwaysOnSampler())
				.AddAspNetCoreInstrumentation()
				.AddHttpClientInstrumentation()
				.AddHotChocolateInstrumentation()
				.AddSqlClientInstrumentation(o =>
				{
					o.RecordException = true;
				})
				.AddZipkinExporter(o => o.Endpoint = new Uri(serverUrl));
		});

		return services;
	}
}