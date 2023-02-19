using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Identity.Extenstion;
using Ukraine.Presentation.GraphQl.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Presentation.Swagger.Extenstion;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class WebApplicationExtensions
{
	public static WebApplication UseExampleApi(this WebApplication application)
	{
		if (application.Environment.IsDevelopment())
			application.UseDeveloperExceptionPage();

		var swaggerOptions = application.Configuration.GetRequiredSection<ExampleSwaggerOptions>(ExampleSwaggerOptions.SECTION_NAME);

		application
			.UseUkraineSwagger(options =>
			{
				options.AddEndpoint();

				if (!string.IsNullOrEmpty(swaggerOptions.OAuthClientId))
					options.AddOAuthClientId(swaggerOptions.OAuthClientId);

				options.AddOAuthAppName(Constants.SERVICE_NAME);
			})
			.UseUkraineAuthentication()
			.UseUkraineAuthorization()
			.UseCloudEvents();

		var graphQlOptions = application.Configuration.GetRequiredSection<ExampleGraphQlOptions>(ExampleGraphQlOptions.SECTION_NAME);

		application.MapSubscribeHandler();

		application.UseUkraineGraphQl(options =>
		{
			options.EnableBananaCakePop = graphQlOptions.EnableBananaCakePop;
		});

		application.MapControllers();

		application.UseUkraineHealthChecks();
		application.UseUkraineDatabaseHealthChecks();

		return application;
	}
}