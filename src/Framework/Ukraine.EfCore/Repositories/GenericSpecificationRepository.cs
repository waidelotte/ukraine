﻿using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ukraine.Domain.Interfaces;
using Ukraine.EfCore.Interfaces;

namespace Ukraine.EfCore.Repositories;

internal class GenericSpecificationRepository<TEntity> : ISpecificationRepository<TEntity>
	where TEntity : class, IAggregateRoot
{
	private readonly IMapper _mapper;
	private readonly DbSet<TEntity> _dbSet;

	public GenericSpecificationRepository(DbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
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

	public async Task<TProject?> GetProjectAsync<TProject>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
	{
		return await ApplySpecification(specification).ProjectTo<TProject>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
	}

	private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
	{
		return SpecificationEvaluator.Default.GetQuery(_dbSet.AsQueryable(), specification);
	}
}