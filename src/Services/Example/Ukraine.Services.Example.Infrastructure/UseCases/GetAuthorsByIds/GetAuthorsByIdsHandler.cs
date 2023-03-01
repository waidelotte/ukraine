using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Framework.EFCore;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;
using Ukraine.Services.Example.Persistence.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorsByIds;

internal sealed class GetAuthorsByIdsHandler : IRequestHandler<GetAuthorsByIdsRequest, GetAuthorsByIdsResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorsByIdsHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GetAuthorsByIdsResponse> Handle(GetAuthorsByIdsRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();

		var authors = await repository
			.GetProjectListAsync<AuthorDTO>(AuthorSpec.Create(request.Ids), cancellationToken);
		return new GetAuthorsByIdsResponse(authors);
	}
}