using Microsoft.Extensions.Options;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.GraphQL.Extenstion;
using Ukraine.Infrastructure.HealthChecks.Extenstion;
using Ukraine.Infrastructure.Swagger.Extenstion;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Api.Extensions;

public static class WebApplicationExtensions
{
	public static WebApplication UseExampleApi(this WebApplication application)
	{
		using (var serviceScope = application.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseFacadeResolver>();
			context.Database.EnsureCreated();
		}

		if (application.Environment.IsDevelopment())
			application.UseDeveloperExceptionPage();

		application
			.UseUkraineSwagger()
			.UseAuthorization()
			.UseCloudEvents();

		application.MapGet("/", () => Results.LocalRedirect("~/graphql"));

		application.MapSubscribeHandler();

		application.UseUkraineGraphQL(application.Services.GetRequiredService<IOptions<ExampleGraphQLOptions>>().Value.IsToolEnabled);

		application.MapControllers();

		application.UseUkraineHealthChecks();
		application.UseUkraineDatabaseHealthChecks();
		
		return application;
	}
}