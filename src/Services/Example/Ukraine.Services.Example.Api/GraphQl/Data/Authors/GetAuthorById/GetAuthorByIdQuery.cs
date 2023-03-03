using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors.GetAuthorById;

[ExtendObjectType(Name = OperationTypeNames.Query)]
internal sealed class GetAuthorByIdQuery
{
	public async Task<AuthorDTO> GetAuthorByIdAsync(
		AuthorsByIdsBatchDataLoader dataLoader,
		[ID(nameof(AuthorDTO))] Guid authorId,
		CancellationToken cancellationToken)
	{
		return await dataLoader.LoadAsync(authorId, cancellationToken);
	}
}