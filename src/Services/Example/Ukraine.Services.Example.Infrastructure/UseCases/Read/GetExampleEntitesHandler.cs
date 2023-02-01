using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ukraine.Services.Example.Infrastructure.DTO;
using Ukraine.Services.Example.Infrastructure.EfCore;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read
{
	public class CreateExampleEntityHandler : IRequestHandler<GetExampleEntitiesRequest, GetExampleEntitiesResponse>
	{
		private readonly ExampleContext _dbContext;
		private readonly IMapper _mapper;

		public CreateExampleEntityHandler(ExampleContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<GetExampleEntitiesResponse> Handle(GetExampleEntitiesRequest request, CancellationToken cancellationToken)
		{
			var entities = await _dbContext.ExampleEntities
				.AsNoTracking()
				.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.ProjectTo<ExampleEntityDTO>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			var total = await _dbContext.ExampleEntities.CountAsync(cancellationToken: cancellationToken);
			
			var response = new GetExampleEntitiesResponse
			{
				Total = total,
				Values = entities
			};

			return response;
		}
	}
}