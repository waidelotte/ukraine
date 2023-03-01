using MediatR;
using Ukraine.Framework.Abstractions;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Authors.GetAuthorQuery;

internal sealed class GetAuthorQueryHandler : IRequestHandler<GetAuthorQueryRequest, GetAuthorQueryResponse>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorQueryHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public Task<GetAuthorQueryResponse> Handle(GetAuthorQueryRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<IRepository<Author>>();
		return Task.FromResult(new GetAuthorQueryResponse(repository.GetQueryProject<AuthorDTO>()));
	}
}