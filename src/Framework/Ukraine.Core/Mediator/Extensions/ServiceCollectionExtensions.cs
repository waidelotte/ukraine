﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ukraine.Core.Mediator.Behaviors;

namespace Ukraine.Core.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUkraineMediatorRequestValidation(this IServiceCollection services)
	{
		services.TryAddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		return services;
	}

	public static IServiceCollection AddUkraineMediatorAndValidators(this IServiceCollection services, Assembly assembly)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}