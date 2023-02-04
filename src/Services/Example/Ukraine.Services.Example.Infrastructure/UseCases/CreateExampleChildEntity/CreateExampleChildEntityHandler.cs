using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

public class CreateExampleChildEntityHandler : IRequestHandler<CreateExampleChildEntityRequest, ExampleChildEntity>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;

	public CreateExampleChildEntityHandler(IUnitOfWork<ExampleContext> unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ExampleChildEntity> Handle(CreateExampleChildEntityRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<ExampleEntity>>();

		var exampleEntity = await repository.GetAsync(ExampleSpec.Create(request.ExampleEntityId), cancellationToken);
		if(exampleEntity == null) throw new ExampleException($"Example Entity {request.ExampleEntityId} not exists");

		var childEntity = new ExampleChildEntity
		{
			NotNullIntValue = request.NotNullIntValue
		};
		
		exampleEntity.ChildEntities.Add(childEntity);
		
		await _unitOfWork.SaveChangesAsync();
		
		return childEntity;
	}
}