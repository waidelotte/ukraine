using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

namespace Ukraine.Services.Example.Api.Graph;

public class Mutations
{
	/// <summary>
	/// Creates a new Author
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<Author> CreateAuthorAsync(
		[Service] IMediator mediator,
		CreateAuthorRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
	
	/// <summary>
	/// Deprecated version of creating a new Author
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<Author> DeprecatedCreateAuthorAsync(
		[Service] IMediator mediator,
		CreateAuthorRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
	
	/// <summary>
	/// Creates a new Author Book
	/// </summary>
	/// <param name="mediator">Injected Mediator by the execution engine</param>
	/// <param name="input">Example Input</param>
	/// <param name="cancellationToken">The cancellation token</param>
	public async Task<Book> CreateBookAsync(
		[Service] IMediator mediator,
		CreateBookRequest input,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(input, cancellationToken);
	}
}