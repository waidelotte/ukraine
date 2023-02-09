using Ukraine.Infrastructure.Configuration.Extensions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Swagger.Extenstion;
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

		using (var serviceScope = application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
			context.Database.EnsureCreated();
		}

		application
			.UseUkraineSwagger()
			.UseAuthorization()
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