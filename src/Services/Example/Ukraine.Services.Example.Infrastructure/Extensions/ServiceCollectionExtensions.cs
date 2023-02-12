using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Mediator.Extensions;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddUkraineMediatorAndValidators(Assembly.GetExecutingAssembly());
		services.AddUkraineMediatorRequestValidation();

		return services;
	}
}