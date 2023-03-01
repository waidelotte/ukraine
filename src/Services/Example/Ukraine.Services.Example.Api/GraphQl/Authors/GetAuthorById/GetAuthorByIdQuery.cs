using Ukraine.Services.Example.Api.GraphQl.DataLoaders;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthorById;

[ExtendObjectType(Name = OperationTypeNames.Query)]
internal sealed class GetAuthorByIdQuery
{
	public async Task<AuthorDTO> GetAuthorByIdAsync(
		AuthorsBatchDataLoader dataLoader,
		[ID(nameof(AuthorDTO))] Guid authorId,
		CancellationToken cancellationToken)
	{
		return await dataLoader.LoadAsync(authorId, cancellationToken);
	}
}