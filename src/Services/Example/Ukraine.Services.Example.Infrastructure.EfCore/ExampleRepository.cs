using AutoMapper;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Repositories;

namespace Ukraine.Services.Example.Infrastructure.EfCore;

public class ExampleRepository<TEntity> : BaseSpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	public ExampleRepository(ExampleContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
}