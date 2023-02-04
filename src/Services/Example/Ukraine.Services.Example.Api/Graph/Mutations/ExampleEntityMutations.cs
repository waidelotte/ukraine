using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

namespace Ukraine.Services.Example.Api.Graph.Mutations;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class ExampleEntityMutations
{
	public async Task<ExampleEntity> CreateExampleEntityAsync(
		[Service] IMediator mediator,
		CreateExampleEntityCommand input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}