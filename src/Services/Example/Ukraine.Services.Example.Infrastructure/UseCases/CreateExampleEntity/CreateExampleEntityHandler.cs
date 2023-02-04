using AutoMapper;
using MediatR;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, CreateExampleEntityResponse>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;

	public CreateExampleEntityHandler(IUnitOfWork<ExampleContext> unitOfWork, IMapper mapper, IEventBus eventBus)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_eventBus = eventBus;
	}

	public async Task<CreateExampleEntityResponse> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
	{
		var exampleEntity = new ExampleEntity(request.StringValue, request.IntValue);

		var repository = _unitOfWork.GetRepository<ISpecificationRepository<ExampleEntity>>();
			
		await repository.AddAsync(exampleEntity, cancellationToken);

		await _unitOfWork.SaveChangesAsync();
		
		var dto = _mapper.Map<ExampleEntityDTO>(exampleEntity);
			
		await _eventBus.PublishAsync(new ExampleEntityCreatedEvent(exampleEntity.Id), cancellationToken);
		
		return new CreateExampleEntityResponse(dto);
	}
}