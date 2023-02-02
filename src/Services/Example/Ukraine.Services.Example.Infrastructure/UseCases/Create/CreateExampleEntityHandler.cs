using AutoMapper;
using MediatR;
using Ukraine.Domain.Abstractions;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Repositories;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create
{
	public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, CreateExampleEntityResponse>
	{
		private readonly IExampleEntityRepository _repository;
		private readonly IMapper _mapper;
		private readonly IEventBus _eventBus;

		public CreateExampleEntityHandler(IExampleEntityRepository repository, IMapper mapper, IEventBus eventBus)
		{
			_repository = repository;
			_mapper = mapper;
			_eventBus = eventBus;
		}

		public async Task<CreateExampleEntityResponse> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
		{
			var exampleEntity = new ExampleEntity(request.StringValue, request.IntValue);
			
			var entity = await _repository.AddAsync(exampleEntity, cancellationToken);
			await _eventBus.PublishAsync(new ExampleEntityCreatedEvent(entity), cancellationToken);
			
			var dto = _mapper.Map<ExampleEntityDTO>(entity);

			return new CreateExampleEntityResponse(dto);
		}
	}
}