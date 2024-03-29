﻿using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ukraine.Framework.Core.Mediator;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddMediatorRequestValidation(this IServiceCollection services)
	{
		services.TryAddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		return services;
	}

	public static IServiceCollection AddMediatorAndValidatorsFromAssembly(
		this IServiceCollection services,
		Assembly assembly)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}