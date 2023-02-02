using AutoMapper;
using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Repositories;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public class CreateExampleEntityHandler : IRequestHandler<GetExampleEntitiesRequest, GetExampleEntitiesResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateExampleEntityHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<GetExampleEntitiesResponse> Handle(GetExampleEntitiesRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<IExampleEntityRepository>();
			
		var entities = await repository.GetAsync(request.PageIndex, request.PageSize, cancellationToken);
		var total = await repository.CountAsync(cancellationToken);

		var response = new GetExampleEntitiesResponse(_mapper.Map<ExampleEntityDTO[]>(entities), total);

		return response;
	}
}