using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Interfaces;
using Ukraine.Infrastructure.EfCore.Repositories;
using Ukraine.Infrastructure.EfCore.Specifications.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Specifications.Repositories;

public class SpecificationRepository<TEntity> : BaseRepository<TEntity>, ISpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	private readonly DbSet<TEntity> _dbSet;

	public SpecificationRepository(DbContext dbContext) : base(dbContext)
	{
		_dbSet = dbContext.Set<TEntity>();
	}

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
		return SpecificationEvaluator.Default.GetQuery(_dbSet.AsQueryable(), specification);
	}
}