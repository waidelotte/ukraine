using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.Identity.Extenstion;
using Ukraine.Presentation.GraphQl.Extenstion;
using Ukraine.Presentation.HealthChecks.Extenstion;
using Ukraine.Presentation.Swagger.Extenstion;
using Ukraine.Services.Example.Api.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class WebApplicationExtensions
{
	public static WebApplication UseExampleApi(this WebApplication application)
	{
		if (application.Environment.IsDevelopment())
		{
			application.UseDeveloperExceptionPage();
		}

		var identityOptions = application.Configuration.GetRequiredSection<ExampleIdentityOptions>(ExampleIdentityOptions.SECTION_NAME);

		application
			.UseUkraineSwagger(options =>
			{
				options.OAuthClientId = identityOptions.SwaggerClientId;
			})
			.UseUkraineAuthentication()
			.UseUkraineAuthorization()
			.UseCloudEvents();

		var graphQlOptions = application.Configuration.GetRequiredSection<ExampleGraphQlOptions>(ExampleGraphQlOptions.SECTION_NAME);

		application.MapGet("/", () => Results.LocalRedirect($"~{graphQlOptions.Path}"));

		application.MapSubscribeHandler();

		if (!string.IsNullOrEmpty(graphQlOptions.Path))
		{
			application.UseUkraineGraphQl(options =>
			{
				options.Path = graphQlOptions.Path;
				options.VoyagerPath = graphQlOptions.VoyagerPath;
				options.UseBananaCakePopTool = true;
			});
		}

		application.MapControllers();

		application.UseUkraineHealthChecks();
		application.UseUkraineDatabaseHealthChecks();

		return application;
	}
}