using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Framework.Core.Mediator;
using Ukraine.Framework.Dapr;

namespace Ukraine.Services.Example.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services
			.AddMediatorAndValidatorsFromAssembly(Assembly.GetExecutingAssembly())
			.AddMediatorRequestValidation()
			.AddAutoMapper(Assembly.GetExecutingAssembly())
			.AddDaprEventBus(options =>
			{
				options.PubSubName = "ukraine-pubsub";
			});

		return services;
	}
}