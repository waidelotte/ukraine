﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ukraine.HealthChecks.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IHealthChecksBuilder AddUkraineHealthChecks(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddHealthChecks();
	}

	public static IHealthChecksBuilder AddUkraineServiceCheck(this IHealthChecksBuilder builder)
	{
		return builder
			.AddCheck(Constants.Tags.SERVICE, () => HealthCheckResult.Healthy(), tags: new[]
			{
				Constants.Tags.READY
			});
	}
}