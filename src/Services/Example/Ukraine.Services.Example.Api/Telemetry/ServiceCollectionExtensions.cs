using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Ukraine.Framework.Core.Options;

namespace Ukraine.Services.Example.Api.Telemetry;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureTelemetry(
		this IServiceCollection serviceCollection,
		IConfiguration configuration)
	{
		var telemetryOptions = serviceCollection
			.BindAndGetOptions<ServiceTelemetryOptions>(configuration.GetSection(ServiceTelemetryOptions.CONFIGURATION_SECTION));

		serviceCollection.AddOpenTelemetry().WithTracing(o =>
		{
			o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(Constants.SERVICE_ID))
				.SetSampler(new AlwaysOnSampler());
			o.AddAspNetCoreInstrumentation();
			o.AddHttpClientInstrumentation();
			o.AddSqlClientInstrumentation(sqlOptions =>
				{
					sqlOptions.RecordException = true;
				});
			o.AddHotChocolateInstrumentation();
			o.AddZipkinExporter(zipkin => zipkin.Endpoint = telemetryOptions.ZipkinServerUrl);
		});

		return serviceCollection;
	}
}