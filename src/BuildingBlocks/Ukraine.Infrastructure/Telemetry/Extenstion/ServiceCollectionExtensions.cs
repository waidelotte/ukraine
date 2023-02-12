using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ukraine.Infrastructure.Telemetry.Options;

namespace Ukraine.Infrastructure.Telemetry.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineTelemetry(
		this IServiceCollection services,
		string serviceName,
		Action<UkraineTelemetryOptions>? options = null)
	{
		var opt = new UkraineTelemetryOptions();
		options?.Invoke(opt);

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
					o.RecordException = opt.RecordSqlException;
				});

			if (!string.IsNullOrEmpty(opt.ZipkinServerUrl))
			{
				builder.AddZipkinExporter(o => o.Endpoint = new Uri(opt.ZipkinServerUrl));
			}
		});

		return services;
	}
}