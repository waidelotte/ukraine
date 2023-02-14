using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Logging.Extenstion;
using Ukraine.Services.Example.Api.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class ConfigureHostBuilderExtensions
{
	public static ConfigureHostBuilder ConfigureHostApi(this ConfigureHostBuilder configureWebHostBuilder, IConfiguration configuration)
	{
		var loggingOptions = configuration.GetRequiredSection<ExampleLoggingOptions>(ExampleLoggingOptions.SECTION_NAME);

		configureWebHostBuilder
			.UseUkraineSerilog(Constants.SERVICE_NAME, options =>
			{
				options.MinimumLevel = loggingOptions.MinimumLevel;
				options.Override(loggingOptions.Override);

				options.WriteTo = writeOptions =>
				{
					writeOptions.WriteToConsole = loggingOptions.WriteToConsole;
					writeOptions.WriteToSeqServerUrl = loggingOptions.WriteToSeqServerUrl;
				};
			})
			.UseUkraineServicesValidationOnBuild();

		return configureWebHostBuilder;
	}
}