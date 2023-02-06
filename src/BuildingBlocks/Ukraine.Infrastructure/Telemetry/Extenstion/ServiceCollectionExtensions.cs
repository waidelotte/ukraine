using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.Telemetry.Options;

namespace Ukraine.Infrastructure.Telemetry.Extenstion;

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
				.AddHotChocolateInstrumentation()
				.AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);

			if (opt.UseZipkin)
			{
				if(string.IsNullOrEmpty(opt.ZipkinEndpoint))
					throw CoreException.NullOrEmpty(nameof(opt.ZipkinEndpoint));
				builder.AddZipkinExporter(o => o.Endpoint = new Uri(opt.ZipkinEndpoint));
			}
		});
		
		return services;
	}
}