using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	public GenericRepository(DbContext dbContext)
	{
		DbSet = dbContext.Set<TEntity>();
	}

	protected DbSet<TEntity> DbSet { get; }

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.AddAsync(entity, cancellationToken);
		return entry.Entity;
	}
}