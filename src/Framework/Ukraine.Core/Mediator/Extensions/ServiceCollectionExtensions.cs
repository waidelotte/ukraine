using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Core.Mediator.Behaviors;

namespace Ukraine.Core.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineMediatorRequestValidation(this IServiceCollection services)
	{
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		return services;
	}

	public static IServiceCollection AddUkraineMediatorAndValidators(this IServiceCollection services, Assembly assembly)
	{
		services.AddMediatR(assembly);
		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}