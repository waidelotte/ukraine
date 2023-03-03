using MediatR;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorsByIds;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors;

public class AuthorsByIdsBatchDataLoader : BatchDataLoader<Guid, AuthorDTO>
{
	private readonly IMediator _mediator;

	public AuthorsByIdsBatchDataLoader(
		IMediator mediator,
		IBatchScheduler batchScheduler,
		DataLoaderOptions? options = null)
		: base(batchScheduler, options)
	{
		_mediator = mediator;
	}

	protected override async Task<IReadOnlyDictionary<Guid, AuthorDTO>> LoadBatchAsync(
		IReadOnlyList<Guid> keys,
		CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(new GetAuthorsByIdsRequest(keys), cancellationToken);
		return response.Authors.ToDictionary(x => x.Id);
	}
}