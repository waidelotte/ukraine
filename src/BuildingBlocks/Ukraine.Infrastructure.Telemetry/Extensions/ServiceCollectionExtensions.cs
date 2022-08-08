using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ukraine.Infrastructure.Telemetry.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomTelemetry(
		this IServiceCollection services,
		string serviceName,
		Action<ZipkinExporterOptions> configureZipkin)
	{
		services.AddOpenTelemetryTracing(builder => builder
			.SetResourceBuilder(ResourceBuilder
				.CreateDefault()
				.AddService(serviceName)) 
			.SetSampler(new AlwaysOnSampler())
			.AddAspNetCoreInstrumentation()
			.AddHttpClientInstrumentation()
			.AddSqlClientInstrumentation(o => o.SetDbStatementForText = true)
			.AddZipkinExporter(configureZipkin));
		
		return services;
	}
}