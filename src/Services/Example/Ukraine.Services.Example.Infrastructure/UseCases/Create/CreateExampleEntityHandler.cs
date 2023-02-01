using AutoMapper;
using MediatR;
using Ukraine.Domain.Abstractions;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create
{
	public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, CreateExampleEntityResponse>
	{
		private readonly ExampleContext _dbContext;
		private readonly IMapper _mapper;
		private readonly IEventBus _eventBus;

		public CreateExampleEntityHandler(ExampleContext dbContext, IMapper mapper, IEventBus eventBus)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_eventBus = eventBus;
		}

		public async Task<CreateExampleEntityResponse> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
		{
			var exampleEntity = new ExampleEntity(request.StringValue, request.IntValue);

			var entry = await _dbContext.ExampleEntities.AddAsync(exampleEntity, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);
			await _eventBus.PublishAsync(new ExampleEntityCreatedEvent(entry.Entity), cancellationToken);
			
			var dto = _mapper.Map<ExampleEntityDTO>(entry.Entity);

			return new CreateExampleEntityResponse(dto);
		}
	}
}