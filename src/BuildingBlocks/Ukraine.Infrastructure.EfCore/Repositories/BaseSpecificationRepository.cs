using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public abstract class BaseSpecificationRepository<TEntity> : BaseRepository<TEntity>, ISpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	public BaseSpecificationRepository(DbContext dbContext) : base(dbContext) { }
	
	public IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification)
	{
		return ApplySpecification(specification);
	}
	
	public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
	}
	
	private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
	{
		return SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);
	}
}