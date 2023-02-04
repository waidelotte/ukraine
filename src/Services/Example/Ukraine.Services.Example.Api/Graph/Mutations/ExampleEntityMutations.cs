using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

namespace Ukraine.Services.Example.Api.Graph.Mutations;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ExampleEntityMutations
{
	[UseMutationConvention]
	public async Task<ExampleEntity> CreateExampleEntityAsync(
		[Service] IMediator mediator,
		string? stringValue, int? intValue,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(new CreateExampleEntityCommand(stringValue, intValue), cancellationToken);
	}
}