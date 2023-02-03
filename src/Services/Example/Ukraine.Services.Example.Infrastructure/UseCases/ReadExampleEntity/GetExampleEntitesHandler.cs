using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public class CreateExampleEntityHandler : IRequestHandler<GetExampleEntitiesRequest, GetExampleEntitiesResponse>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;

	public CreateExampleEntityHandler(IUnitOfWork<ExampleContext> unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GetExampleEntitiesResponse> Handle(GetExampleEntitiesRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<Domain.Entities.ExampleEntity>();
			
		var entities = await repository
			.ProjectListAsync<ExampleEntityDTO>(ExampleSpec.Create(request.PageIndex, request.PageSize), cancellationToken);
		
		var total = await repository.CountAsync(ExampleSpec.Create(), cancellationToken);

		var response = new GetExampleEntitiesResponse(entities, total);

		return response;
	}
}