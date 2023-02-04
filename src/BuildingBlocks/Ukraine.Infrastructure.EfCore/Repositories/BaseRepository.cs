using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> 
	where TEntity : class, IAggregateRoot
{
	protected readonly DbSet<TEntity> DbSet;

	public BaseRepository(DbContext dbContext)
	{
		DbSet = dbContext.Set<TEntity>();
	}
	
	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.AddAsync(entity, cancellationToken);
		return entry.Entity;
	}
}