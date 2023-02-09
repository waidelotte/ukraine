using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Repositories;

namespace Ukraine.Services.Example.Infrastructure.EfCore;

internal class ExampleRepository<TEntity> : BaseSpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	public ExampleRepository(ExampleContext dbContext) : base(dbContext) { }
}