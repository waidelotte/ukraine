using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

namespace Ukraine.Services.Example.Api.Graph;

public class Queries
{
	/// <summary>
	/// Gets a list of Entities
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<IQueryable<ExampleEntity>> GetExampleEntitiesAsync([FromServices] IMediator mediator, CancellationToken cancellationToken)
	{
		return await mediator.Send(new GetExampleEntitiesQueryRequest(), cancellationToken); 
	}
}