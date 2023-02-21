using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ukraine.Telemetry.Options;

namespace Ukraine.Telemetry.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineTelemetry(
		this IServiceCollection services,
		IConfigurationSection configurationSection)
	{
		services.AddOptions<UkraineTelemetryOptions>()
			.Bind(configurationSection)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var options = configurationSection.Get<UkraineTelemetryOptions>(options =>
		{
			options.ErrorOnUnknownConfiguration = true;
		});

		if (options == null)
			throw new ArgumentNullException(nameof(configurationSection), $"Configuration Section [{configurationSection.Key}] is empty");

		services.AddOpenTelemetryTracing(o =>
		{
			o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(options.ServiceName))
				.SetSampler(new AlwaysOnSampler());

			if (options.Instrumentation.AspNetCore)
				o.AddAspNetCoreInstrumentation();

			if (options.Instrumentation.HttpClient)
				o.AddHttpClientInstrumentation();

			if (options.Instrumentation.SqlClient)
			{
				o.AddSqlClientInstrumentation(sqlOptions =>
				{
					sqlOptions.RecordException = true;
				});
			}

			if (options.Instrumentation.HotChocolate)
				o.AddHotChocolateInstrumentation();

			if (options.Exporter.Zipkin != null)
				o.AddZipkinExporter(zipkin => zipkin.Endpoint = options.Exporter.Zipkin);
		});

		return services;
	}
}