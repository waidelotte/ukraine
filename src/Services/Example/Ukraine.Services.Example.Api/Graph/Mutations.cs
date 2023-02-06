using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

namespace Ukraine.Services.Example.Api.Graph;

public class Mutations
{
	/// <summary>
	/// Creates a new Entity
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<ExampleEntity> CreateExampleEntityAsync(
		[Service] IMediator mediator,
		CreateExampleEntityRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
	
	/// <summary>
	/// Deprecated version of creating a new entity
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<ExampleEntity> DeprecatedCreateExampleEntityAsync(
		[Service] IMediator mediator,
		CreateExampleEntityRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
	
	/// <summary>
	/// Creates a new Child Entity
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<ExampleChildEntity> CreateExampleChildEntityAsync(
		[Service] IMediator mediator,
		CreateExampleChildEntityRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}