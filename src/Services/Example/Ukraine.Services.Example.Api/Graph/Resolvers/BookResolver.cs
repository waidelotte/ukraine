using MediatR;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

namespace Ukraine.Services.Example.Api.Graph.Resolvers;

public class BookResolver
{
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