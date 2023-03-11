using Ukraine.Framework.Core.HealthChecks;
using Ukraine.Framework.Core.Host;
using Ukraine.Framework.Core.Options;
using Ukraine.Framework.Core.Serilog;
using Ukraine.Gateway.GraphQl.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

builder.Host.UseSerilog(configuration);
builder.Host.AddServicesValidationOnBuild();

services.AddHeaderPropagation(o => o.Headers.Add("Authorization"));

var schemas = configuration.GetRequiredSection("GraphQlSchemas").GetOptions<GraphQlSchemaOptions[]>();
var graphQlServer = services
	.AddGraphQLServer();

foreach (var schema in schemas)
{
	services
		.AddHttpClient(schema.Name, c => c.BaseAddress = schema.Url)
		.AddHeaderPropagation(options => options.Headers.Add("Authorization"));

	graphQlServer.AddRemoteSchema(schema.Name);
}

services
	.AddHealthChecks()
	.AddDefaultCheck("Gateway", new[] { "gateway", "graphql" });

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseHeaderPropagation();

app.MapGraphQL("/");
app.MapDefaultHealthChecks();

app.Run();