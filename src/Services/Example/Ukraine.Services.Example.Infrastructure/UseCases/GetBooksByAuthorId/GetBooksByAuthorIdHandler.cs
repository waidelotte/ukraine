using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetBooksByAuthorId;

internal sealed class GetBooksByAuthorIdHandler : IRequestHandler<GetBooksByAuthorIdRequest, GetBooksByAuthorIdResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBooksByAuthorIdHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GetBooksByAuthorIdResponse> Handle(GetBooksByAuthorIdRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Book>>();

		var book = await repository
			.GetProjectListAsync<BookDTO>(BookByAuthorSpec.Create(request.AuthorId), cancellationToken);
		return new GetBooksByAuthorIdResponse(book);
	}
}