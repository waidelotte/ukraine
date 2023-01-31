using AutoMapper;
using MediatR;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create
{
	public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityRequest, CreateExampleEntityResponse>
	{
		private readonly ExampleContext _dbContext;
		private readonly IMapper _mapper;

		public CreateExampleEntityHandler(ExampleContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CreateExampleEntityResponse> Handle(CreateExampleEntityRequest request, CancellationToken cancellationToken)
		{
			var exampleEntity = new ExampleEntity
			{
				StringValue = request.StringValue,
				IntValue = request.IntValue
			};

			var entry = await _dbContext.ExampleEntity.AddAsync(exampleEntity, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var dto = _mapper.Map<ExampleEntityDTO>(entry.Entity);

			return new CreateExampleEntityResponse(dto);
		}
	}
}