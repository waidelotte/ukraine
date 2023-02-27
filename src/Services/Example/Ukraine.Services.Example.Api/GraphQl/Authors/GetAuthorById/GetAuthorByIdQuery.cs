using MediatR;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthorById;

[ExtendObjectType(Name = OperationTypeNames.Query)]
internal sealed class GetAuthorByIdQuery
{
	public async Task<GetAuthorByIdResponse> GetAuthorByIdAsync(
		IMediator mediator,
		[ID(nameof(AuthorDTO))] Guid authorId,
		CancellationToken cancellationToken)
	{
		return await mediator.Send(new GetAuthorByIdRequest(authorId), cancellationToken);
	}
}