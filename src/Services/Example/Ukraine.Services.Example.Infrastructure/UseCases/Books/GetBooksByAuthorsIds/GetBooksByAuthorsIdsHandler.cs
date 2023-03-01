using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Books.GetBooksByAuthorsIds;

internal sealed class GetBooksByAuthorsIdsHandler : IRequestHandler<GetBooksByAuthorsIdsRequest, GetBooksByAuthorsIdsResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBooksByAuthorsIdsHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GetBooksByAuthorsIdsResponse> Handle(GetBooksByAuthorsIdsRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Book>>();

		var books = await repository
			.GetProjectListAsync<BookDTO>(BookByAuthorSpec.Create(request.AuthorsIds), cancellationToken);

		return new GetBooksByAuthorsIdsResponse(books);
	}
}