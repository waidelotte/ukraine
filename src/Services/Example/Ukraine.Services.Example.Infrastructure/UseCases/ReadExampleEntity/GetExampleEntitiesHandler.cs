using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public class GetExampleEntitiesHandler : IRequestHandler<GetExampleEntitiesQuery, IQueryable<ExampleEntity>>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;

	public GetExampleEntitiesHandler(IUnitOfWork<ExampleContext> unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public Task<IQueryable<ExampleEntity>> Handle(GetExampleEntitiesQuery request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<ExampleEntity>>();
		var entities = repository.GetQuery(ExampleSpec.Create());
		return Task.FromResult(entities);
	}
}