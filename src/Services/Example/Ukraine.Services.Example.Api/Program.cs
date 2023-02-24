using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Types.Pagination;
using MediatR;
using Ukraine.Core.Extensions;
using Ukraine.Dapr.Extensions;
using Ukraine.EfCore.Extensions;
using Ukraine.GraphQl.Extenstion;
using Ukraine.HealthChecks.Extenstion;
using Ukraine.Identity.Extenstion;
using Ukraine.Logging.Extenstion;
using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.Graph.Types.Author;
using Ukraine.Services.Example.Api.Graph.Types.Book;
using Ukraine.Services.Example.Api.Graph.Types.User;
using Ukraine.Services.Example.Infrastructure.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;
using Ukraine.Services.Example.Persistence;
using Ukraine.Services.Example.Persistence.Extensions;
using Ukraine.Swagger.Extenstion;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var isDevelopment = builder.Environment.IsDevelopment();

configuration.AddUkraineDaprSecretStore("ukraine-secretstore");

services.AddInfrastructure(configuration);
services.AddPersistence(configuration);

builder.Host.AddUkraineSerilog(services, configuration.GetSection("UkraineLogging"));
builder.Host.AddServicesValidationOnBuild();

services.AddControllers();
services.AddUkraineSwagger(configuration.GetSection("UkraineSwagger"));

services.AddAuthorization(o =>
{
	o.AddScopePolicy(Constants.Policy.REST_API, new[] { Constants.Scope.REST_SCOPE });
	o.AddScopePolicy(Constants.Policy.GRAPHQL_API, new[] { Constants.Scope.GRAPHQL_SCOPE });
});

var authenticationOptions = services
	.BindAndGetOptions<AuthenticationOptions>(configuration.GetSection(Constants.ConfigurationSection.AUTHENTICATION));

services.AddJwtBearerAuthentication(o =>
{
	o.Audience = Constants.SERVICE_ID;
	o.Authority = authenticationOptions.Authority;
	o.RequireHttpsMetadata = !isDevelopment;

	o.TokenValidationParameters.ValidateAudience = true;
	o.TokenValidationParameters.ValidateIssuer = true;
	o.TokenValidationParameters.ValidateIssuerSigningKey = true;
});

var graphQlOptions = services
	.BindAndGetOptions<GraphQlOptions>(configuration.GetSection(Constants.ConfigurationSection.GRAPHQL));

services
	.AddGraphQlServer<ExampleContext>()
	.ConfigureDefaultRoot()
	.AddAuthorization()
	.AddFiltering()
	.AddProjections()
	.AddSorting()
	.AllowIntrospection(graphQlOptions.EnableIntrospection)
	.ModifyRequestOptions(o =>
	{
		o.IncludeExceptionDetails = graphQlOptions.EnableExceptionDetails;
		o.ExecutionTimeout = graphQlOptions.ExecutionTimeout;
	})
	.SetPagingOptions(new PagingOptions
	{
		MaxPageSize = graphQlOptions.Paging?.MaxPageSize,
		DefaultPageSize = graphQlOptions.Paging?.DefaultPageSize,
		IncludeTotalCount = graphQlOptions.Paging?.IncludeTotalCount,
		AllowBackwardPagination = graphQlOptions.Paging?.AllowBackwardPagination
	})
	.AddMaxExecutionDepthRule(graphQlOptions.ExecutionMaxDepth, true)
	.AddInstrumentation(o =>
	{
		o.IncludeDataLoaderKeys = true;
		o.RenameRootActivity = true;
	})
	.AddType<UserQueryType>()
	.AddType<AuthorQueryType>()
	.AddType<AuthorMutationTypeExtension>()
	.AddType<BookMutationType>()
	.RegisterService<IMediator>(ServiceKind.Synchronized)
	.InitializeOnStartup();

var app = builder.Build();

await app.Services.MigrateDatabaseAsync<ExampleContext>();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseUkraineSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.UseUkraineDaprEventBus();

app.MapGraphQL(graphQlOptions.Path).WithOptions(new GraphQLServerOptions
{
	EnableSchemaRequests = graphQlOptions.EnableSchemaRequests,
	EnableGetRequests = graphQlOptions.EnableGetRequests,
	EnableMultipartRequests = graphQlOptions.EnableMultipartRequests,
	Tool = { Enable = graphQlOptions.EnableBananaCakePop },
	EnableBatching = graphQlOptions.EnableBatching
});

app.MapControllers();
app.UseUkraineHealthChecks();
app.UseUkraineDatabaseHealthChecks();

try
{
	app.Logger.LogInformation("Starting Web Host [service-example-api]");
	app.Run();
}
catch (Exception ex)
{
	app.Logger.LogCritical(ex, "Host terminated unexpectedly [service-example-api]");
}
finally
{
	Serilog.Log.CloseAndFlush();
}