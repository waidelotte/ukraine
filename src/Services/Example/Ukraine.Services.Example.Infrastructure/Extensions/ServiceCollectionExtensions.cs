using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Mediator.Extensions;
using Ukraine.Services.Example.Infrastructure.Options;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExampleInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<ExampleApplicationOptions>(configuration);
		services.Configure<ExampleLoggingOptions>(configuration.GetSection(ExampleLoggingOptions.SectionName));
		services.Configure<ExampleTelemetryOptions>(configuration.GetSection(ExampleTelemetryOptions.SectionName));
		services.Configure<ExampleHealthCheckOptions>(configuration.GetSection(ExampleHealthCheckOptions.SectionName));

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddCustomMediatorFluentValidation();
		services.AddCustomPagedRequestValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		
		return services;
	}
}