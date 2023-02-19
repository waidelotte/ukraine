using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Hosting.Extensions;
using Ukraine.Infrastructure.Serilog.Extenstion;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class ConfigureHostBuilderExtensions
{
	public static ConfigureHostBuilder ConfigureHostApi(this ConfigureHostBuilder configureWebHostBuilder, IConfiguration configuration)
	{
		var loggingOptions = configuration.GetRequiredSection<ExampleLoggingOptions>(ExampleLoggingOptions.SECTION_NAME);

		configureWebHostBuilder
			.UseUkraineSerilog(options =>
			{
				options.ServiceName = Constants.SERVICE_NAME;
				options.MinimumLevel = loggingOptions.MinimumLevel;
				options.Override(loggingOptions.Override);

				options.WriteTo = writeOptions =>
				{
					writeOptions.WriteToSeqServerUrl = loggingOptions.WriteToSeqServerUrl;
				};
			})
			.UseUkraineServicesValidationOnBuild();

		return configureWebHostBuilder;
	}
}