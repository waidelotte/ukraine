using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Infrastructure.EfCore.Specifications.Interfaces;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadAuthor;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQueryRequest, IQueryable<Author>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public Task<IQueryable<Author>> Handle(GetAuthorsQueryRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<Author>>();
		var query = repository.GetQuery(AuthorSpec.Create());
		return Task.FromResult(query);
	}
}