using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

public class GenericRepository<TEntity> : IRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	private readonly IMapper _mapper;

	public GenericRepository(DbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		DbSet = dbContext.Set<TEntity>();
	}

	protected DbSet<TEntity> DbSet { get; }

	public IQueryable<TEntity> GetQuery()
	{
		return DbSet.AsQueryable();
	}

	public IQueryable<TProject> GetQueryProject<TProject>()
	{
		return DbSet.ProjectTo<TProject>(_mapper.ConfigurationProvider).AsQueryable();
	}

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await DbSet.AddAsync(entity, cancellationToken);
		return entry.Entity;
	}
}