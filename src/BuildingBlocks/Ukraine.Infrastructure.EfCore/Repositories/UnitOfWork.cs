using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Repositories;

internal sealed class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
	where TDbContext : DbContext
{
	private readonly TDbContext _context;
	private readonly IServiceProvider _serviceProvider;
	private Dictionary<Type, IRepository>? _repositories;

	public UnitOfWork(TDbContext context, IServiceProvider serviceProvider)
	{
		_context = context;
		_serviceProvider = serviceProvider;
	}

	public TRepository GetRepository<TRepository>()
		where TRepository : IRepository
	{
		_repositories ??= new Dictionary<Type, IRepository>();

		var type = typeof(IRepository);

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

	public int SaveChanges()
	{
		return _context.SaveChanges();
	}

	public async Task<int> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync();
	}
}