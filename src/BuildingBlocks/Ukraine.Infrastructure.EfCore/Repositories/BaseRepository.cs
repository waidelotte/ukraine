using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public class BaseRepository<TDbContext, TEntity> : IRepository<TDbContext, TEntity> 
	where TEntity : class, IAggregateRoot
	where TDbContext : DbContext
{
	private readonly IMapper _mapper;
	private readonly DbSet<TEntity> _dbSet;

	public BaseRepository(TDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbSet = dbContext.Set<TEntity>();
	}
	
	public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

		return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
	}
	
	public async Task<List<TProject>> ProjectListAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		return await ApplySpecification(specification).ProjectTo<TProject>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
	}
	
	public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		return await ApplySpecification(specification).CountAsync(cancellationToken);
	}
	
	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await _dbSet.AddAsync(entity, cancellationToken);
		return entry.Entity;
	}
	
	private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
	{
		return SpecificationEvaluator.Default.GetQuery(_dbSet.AsQueryable(), specification);
	}
}