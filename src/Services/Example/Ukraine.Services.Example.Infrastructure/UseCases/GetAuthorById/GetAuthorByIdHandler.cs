using MediatR;
using Ukraine.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdRequest, GetAuthorByIdResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorByIdHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GetAuthorByIdResponse> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var author = await repository.GetProjectAsync<AuthorDTO>(AuthorSpec.Create(request.AuthorId), cancellationToken);
		return new GetAuthorByIdResponse(author);
	}
}