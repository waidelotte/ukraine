using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.GraphQl.Extenstion;

public static class ServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddGraphQlServer<TContext>(this IServiceCollection serviceCollection)
		where TContext : DbContext
	{
		return serviceCollection.AddGraphQLServer().RegisterDbContext<TContext>();
	}
}