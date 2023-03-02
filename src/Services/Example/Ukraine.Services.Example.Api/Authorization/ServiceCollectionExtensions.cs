namespace Ukraine.Services.Example.Api.Authorization;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureAuthorization(this IServiceCollection serviceCollection)
	{
		return serviceCollection.AddAuthorization(options =>
		{
			options.AddPolicy(Constants.Policy.REST_API, builder =>
			{
				builder.RequireClaim("scope", Constants.Scope.REST_SCOPE);
			});

			options.AddPolicy(Constants.Policy.GRAPHQL_API, builder =>
			{
				builder.RequireClaim("scope", Constants.Scope.GRAPHQL_SCOPE);
			});

			options.AddPolicy(Constants.Policy.GRAPHQL_ADMIN, builder =>
			{
				builder.RequireRole(Constants.Role.ADMIN);
				builder.RequireClaim("scope", Constants.Scope.GRAPHQL_SCOPE);
			});
		});
	}
}