using AutoMapper;
using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public class CreateExampleEntityHandler : IRequestHandler<GetExampleEntitiesRequest, GetExampleEntitiesResponse>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;
	private readonly IMapper _mapper;

	public CreateExampleEntityHandler(IUnitOfWork<ExampleContext> unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<GetExampleEntitiesResponse> Handle(GetExampleEntitiesRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ExampleEntity>();
			
		var entities = await repository
			.ListAsync(ExampleSpec.Create(request.PageIndex, request.PageSize), cancellationToken);
		
		var total = await repository.CountAsync(ExampleSpec.Create(), cancellationToken);

		var response = new GetExampleEntitiesResponse(_mapper.Map<ExampleEntityDTO[]>(entities), total);

		return response;
	}
}