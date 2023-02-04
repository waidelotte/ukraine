using AutoMapper;
using MediatR;
using Ukraine.Infrastructure.EfCore.Interfaces;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Domain.Exceptions;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;
using Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;

public class CreateExampleChildEntityHandler : IRequestHandler<CreateExampleChildEntityRequest, CreateExampleChildEntityResponse>
{
	private readonly IUnitOfWork<ExampleContext> _unitOfWork;
	private readonly IMapper _mapper;

	public CreateExampleChildEntityHandler(IUnitOfWork<ExampleContext> unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<CreateExampleChildEntityResponse> Handle(CreateExampleChildEntityRequest request, CancellationToken cancellationToken)
	{
		var repository = _unitOfWork.GetRepository<ISpecificationRepository<ExampleEntity>>();

		var exampleEntity = await repository.GetAsync(ExampleSpec.Create(request.ExampleEntityId), cancellationToken);
		if(exampleEntity == null) throw new ExampleException($"Example Entity {request.ExampleEntityId} not exists");

		var childEntity = new ExampleChildEntity(request.NotNullIntValue);
		exampleEntity.ChildEntities.Add(childEntity);
		
		await _unitOfWork.SaveChangesAsync();
		
		var dto = _mapper.Map<ExampleChildEntityDTO>(childEntity);
		
		return new CreateExampleChildEntityResponse(dto);
	}
}