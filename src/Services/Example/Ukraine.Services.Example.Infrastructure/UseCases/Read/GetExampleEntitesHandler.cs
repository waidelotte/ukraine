using AutoMapper;
using MediatR;
using Ukraine.Services.Example.Domain.Repositories;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read
{
	public class CreateExampleEntityHandler : IRequestHandler<GetExampleEntitiesRequest, GetExampleEntitiesResponse>
	{
		private readonly IExampleEntityRepository _repository;
		private readonly IMapper _mapper;

		public CreateExampleEntityHandler(IExampleEntityRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<GetExampleEntitiesResponse> Handle(GetExampleEntitiesRequest request, CancellationToken cancellationToken)
		{
			var entities = await _repository.GetAsync(request.PageIndex, request.PageSize, cancellationToken);
			var total = await _repository.CountAsync(cancellationToken);

			var response = new GetExampleEntitiesResponse(_mapper.Map<ExampleEntityDTO[]>(entities), total);

			return response;
		}
	}
}