using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.Presentation.GraphQl.Extenstion;

public static class RequestExecutorBuilderExtensions
{
	public static IRequestExecutorBuilder AddUkraineEfCore<TContext>(this IRequestExecutorBuilder builder)
		where TContext : DbContext
	{
		builder
			.RegisterDbContext<TContext>()
			.AddProjections()
			.AddSorting();

		return builder;
	}
}