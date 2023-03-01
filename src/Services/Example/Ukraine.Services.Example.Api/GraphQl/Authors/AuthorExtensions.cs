using MediatR;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Infrastructure.UseCases.GetBooksByAuthorId;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

[ExtendObjectType(typeof(AuthorDTO))]
internal sealed class AuthorExtensions
{
	public async Task<IEnumerable<BookDTO>> Books(
		[Parent] AuthorDTO author, IMediator mediator, CancellationToken cancellationToken)
	{
		var response = await mediator.Send(new GetBooksByAuthorIdRequest(author.Id), cancellationToken);
		return response.Books;
	}
}