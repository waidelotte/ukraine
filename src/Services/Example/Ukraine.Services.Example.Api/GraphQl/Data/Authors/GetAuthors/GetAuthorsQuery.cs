using MediatR;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorQuery;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors.GetAuthors;

[ExtendObjectType(Name = OperationTypeNames.Query)]
internal sealed class GetAuthorsQuery
{
	[UsePaging]
	[UseProjection]
	[UseFiltering]
	[UseSorting]
	public async Task<IQueryable<AuthorDTO>> GetAuthorsAsync(
		IMediator mediator,
		CancellationToken cancellationToken)
	{
		var response = await mediator.Send(new GetAuthorQueryRequest(), cancellationToken);
		return response.Query;
	}
}