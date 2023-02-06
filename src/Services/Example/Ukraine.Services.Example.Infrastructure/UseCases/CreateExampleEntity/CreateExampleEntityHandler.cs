using MediatR;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;

public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, ExampleEntity>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;
	private readonly IEventBus _eventBus;

	public CreateExampleEntityHandler(IUnitOfWork<ExampleContext> unitOfWork, IEventBus eventBus)
	{
		_unitOfWork = unitOfWork;
		_eventBus = eventBus;
	}

	public async Task<ExampleEntity> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
	{
		var exampleEntity = new ExampleEntity
		{
			StringValue = request.StringValue,
			IntValue = request.IntValue,
			SuperSecretKey = Guid.NewGuid()
		};
		
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<ExampleEntity>>();
			
		await repository.AddAsync(exampleEntity, cancellationToken);

		await _unitOfWork.SaveChangesAsync();
		
		await _eventBus.PublishAsync(new ExampleEntityCreatedEvent(exampleEntity.Id), cancellationToken);
		
		return exampleEntity;
	}
}