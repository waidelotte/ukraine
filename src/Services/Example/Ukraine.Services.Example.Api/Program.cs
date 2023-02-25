using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Types.Pagination;
using MediatR;
using Ukraine.Core.Host.Extensions;
using Ukraine.Core.Logging.Extenstion;
using Ukraine.Core.Options.Extensions;
using Ukraine.Dapr.Extensions;
using Ukraine.EfCore.Extensions;
using Ukraine.GraphQl.Extenstion;
using Ukraine.HealthChecks.Extenstion;
using Ukraine.Identity.Extenstion;
using Ukraine.Services.Example.Api;
using Ukraine.Services.Example.Api.GraphQl.Authors;
using Ukraine.Services.Example.Api.GraphQl.Authors.CreateAuthor;
using Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthorById;
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
services.AddHttpContextAccessor();
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
	.ModifyOptions(opt => opt.UseXmlDocumentation = true)
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
	.AddMutationConventions(applyToAllMutations: true)
	.RegisterService<IMediator>(ServiceKind.Synchronized)
	.AddType<AuthorType>()
	.AddTypeExtension<AuthorExtensions>()
	.AddType<CreateAuthorMutation>()
	.AddType<CreateAuthorInputType>()
	.AddType<CreateAuthorPayloadType>()
	.AddType<GetAuthorByIdQuery>()
	.AddType<GetAuthorByIdPayloadType>()
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