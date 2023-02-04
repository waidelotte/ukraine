using Ardalis.Specification;
using Ukraine.Domain.Abstractions;

namespace Ukraine.Infrastructure.EfCore.Interfaces;

public interface ISpecificationRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification);
	Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}