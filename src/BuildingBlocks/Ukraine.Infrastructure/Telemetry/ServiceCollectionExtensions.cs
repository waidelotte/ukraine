using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ukraine.Infrastructure.Telemetry;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomTelemetry(
		this IServiceCollection services,
		Action<CustomTelemetryOptions> options)
	{
		var opt = new CustomTelemetryOptions();
		options.Invoke(opt);

		services.AddOpenTelemetryTracing(builder =>
		{
			builder
				.SetResourceBuilder(ResourceBuilder
					.CreateDefault()
					.AddService(opt.ApplicationName))
				.SetSampler(new AlwaysOnSampler())
				.AddAspNetCoreInstrumentation()
				.AddHttpClientInstrumentation()
				.AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);
			
			if (opt.UseZipkin)
				builder.AddZipkinExporter(o => o.Endpoint = new Uri(opt.ZipkinEndpoint));
		});
		
		return services;
	}
}