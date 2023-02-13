using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Exceptions;
using Ukraine.Domain.Interfaces;

namespace Ukraine.Persistence.EfCore;

internal sealed class UnitOfWork : IUnitOfWork
{
	private readonly DbContext _context;
	private readonly IServiceProvider _serviceProvider;
	private Dictionary<Type, IRepository>? _repositories;

	public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
	{
		_context = context;
		_serviceProvider = serviceProvider;
	}

	public TRepository GetRepository<TRepository>()
		where TRepository : IRepository
	{
		_repositories ??= new Dictionary<Type, IRepository>();

		var type = typeof(TRepository);

		if (_repositories.TryGetValue(type, out var repository))
		{
			return (TRepository)repository;
		}

		var serviceRepository = _serviceProvider.GetService<TRepository>();
		if (serviceRepository == null)
		{
			throw CoreException.Exception($"Repository of type {typeof(TRepository)} not found.");
		}

		_repositories.TryAdd(type, serviceRepository);

		return serviceRepository;
	}

	public bool SaveChanges()
	{
		return _context.SaveChanges(false) > 0;
	}

	public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken) > 0;
	}
}