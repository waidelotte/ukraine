using AutoMapper;
using MediatR;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Repositories;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create;

public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, CreateExampleEntityResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;

	public CreateExampleEntityHandler(IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_eventBus = eventBus;
	}

	public async Task<CreateExampleEntityResponse> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
	{
		var exampleEntity = new ExampleEntity(request.StringValue, request.IntValue);

		var repository = _unitOfWork.GetRepository<IExampleEntityRepository>();
			
		await repository.AddAsync(exampleEntity, cancellationToken);

		await _unitOfWork.SaveChangesAsync();
			
		await _eventBus.PublishAsync(new ExampleEntityCreatedEvent(exampleEntity), cancellationToken);
			
		var dto = _mapper.Map<ExampleEntityDTO>(exampleEntity);

		return new CreateExampleEntityResponse(dto);
	}
}