using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Abstractions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

public abstract class BaseSpecificationRepository<TEntity> : BaseRepository<TEntity>, ISpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	private readonly IMapper _mapper;
	
	public BaseSpecificationRepository(DbContext dbContext, IMapper mapper) : base(dbContext)
	{
		_mapper = mapper;
	}
	
	public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
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
	
	private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
	{
		return SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);
	}
}