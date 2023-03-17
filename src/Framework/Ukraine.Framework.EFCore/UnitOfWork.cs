using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.EFCore;

internal sealed class UnitOfWork : IUnitOfWork
{
	private readonly DbContext _context;
	private readonly IServiceProvider _serviceProvider;
	private Dictionary<Type, IRepository>? _repositories;
	private bool _disposed;

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

		var serviceRepository = _serviceProvider.GetRequiredService<TRepository>();

		_repositories.TryAdd(type, serviceRepository);

		return serviceRepository;
	}

	public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken) > 0;
	}

	public void Rollback()
	{
		_context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
	}

	public void Dispose()
	{
		Dispose(true);

		// ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}

		_disposed = true;
	}
}