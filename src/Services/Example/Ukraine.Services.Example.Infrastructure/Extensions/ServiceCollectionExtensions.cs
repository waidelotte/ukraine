using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Dapr.Extensions;
using Ukraine.Infrastructure.Mediator.Extensions;
using Ukraine.Infrastructure.Telemetry.Extenstion;
using Ukraine.Infrastructure.Telemetry.Options;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		string serviceName,
		IConfiguration configuration)
	{
		var telemetryOptions = configuration.GetRequiredSection<ExampleTelemetryOptions>(ExampleTelemetryOptions.SECTION_NAME);

		services
			.AddUkraineMediatorAndValidators(Assembly.GetExecutingAssembly())
			.AddUkraineMediatorRequestValidation()
			.AddUkraineTelemetry(serviceName, options =>
			{
				options.AddSql(telemetryOptions.RecordSqlException);
				options.AddHotChocolate();

				if (!string.IsNullOrEmpty(telemetryOptions.ZipkinServerUrl))
					options.AddZipkinExporter(telemetryOptions.ZipkinServerUrl);
			})
			.AddUkraineDaprEventBus();

		return services;
	}
}