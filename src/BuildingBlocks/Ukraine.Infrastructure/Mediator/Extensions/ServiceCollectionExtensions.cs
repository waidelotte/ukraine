using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Mediator.Behaviors;

namespace Ukraine.Infrastructure.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomMediatorFluentValidation(this IServiceCollection services)
	{
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		
		return services;
	}
}