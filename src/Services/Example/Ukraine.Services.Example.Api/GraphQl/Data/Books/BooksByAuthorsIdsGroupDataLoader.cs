using MediatR;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorsIds;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Books;

public class BooksByAuthorsIdsGroupDataLoader : GroupedDataLoader<Guid, BookDTO>
{
	private readonly IMediator _mediator;

	public BooksByAuthorsIdsGroupDataLoader(
		IMediator mediator,
		IBatchScheduler batchScheduler,
		DataLoaderOptions? options = null)
		: base(batchScheduler, options)
	{
		_mediator = mediator;
	}

	protected override async Task<ILookup<Guid, BookDTO>> LoadGroupedBatchAsync(
		IReadOnlyList<Guid> keys,
		CancellationToken cancellationToken)
	{
		var response = await _mediator.Send(new GetBooksByAuthorsIdsRequest(keys), cancellationToken);
		return response.Books.ToLookup(x => x.AuthorId);
	}
}