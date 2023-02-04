using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

namespace Ukraine.Services.Example.Api.Graph.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class ExampleEntityQueries
{
	[UsePaging]
	[UseProjection]
	public async Task<IQueryable<ExampleEntity>> GetExampleEntitiesAsync([Service] IMediator mediator, CancellationToken cancellationToken)
	{
		return await mediator.Send(new GetExampleEntitiesQuery(), cancellationToken);
	}
}