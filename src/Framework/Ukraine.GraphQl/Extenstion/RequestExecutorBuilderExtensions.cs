using HotChocolate.Execution.Configuration;
using HotChocolate.Language;
using Microsoft.Extensions.DependencyInjection;

namespace Ukraine.GraphQl.Extenstion;

public static class RequestExecutorBuilderExtensions
{
	public static IRequestExecutorBuilder ConfigureDefaultRoot(this IRequestExecutorBuilder builder)
	{
		return builder.ConfigureSchema(schemaBuilder =>
		{
			schemaBuilder.TryAddRootType(
				() => new ObjectType(d =>
					d.Name(OperationTypeNames.Query)),
				OperationType.Query);
			schemaBuilder.TryAddRootType(
				() => new ObjectType(d =>
					d.Name(OperationTypeNames.Mutation)),
				OperationType.Mutation);
		});
	}
}