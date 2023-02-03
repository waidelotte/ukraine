using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Infrastructure.Mediator.Behaviors;
using Ukraine.Infrastructure.Mediator.Validation;

namespace Ukraine.Infrastructure.Mediator.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomMediatorFluentValidation(this IServiceCollection services)
	{
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		
		return services;
	}
	
	public static IServiceCollection AddCustomPagedRequestValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
	{
		var validatorTypes = assembly
			.GetExportedTypes()
			.Where(t => t is { IsClass: true, IsAbstract: false, BaseType.IsGenericType: true })
			.Select(t => new
			{
				BaseGenericType = t.BaseType,
				CurrentType = t
			})
			.Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(PagedRequestValidator<,>))
			.ToList();
        
		foreach (var validatorType in validatorTypes)
		{
			var validatorTypeGenericArguments = validatorType.BaseGenericType?.GetGenericArguments().ToList();
        
			if (validatorTypeGenericArguments == null) continue;
                
			var validatorServiceType = typeof(IValidator<>).MakeGenericType(validatorTypeGenericArguments.First());
			services.AddScoped(validatorServiceType, validatorType.CurrentType);
		}
        
		return services;
	}
}