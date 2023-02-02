using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Abstractions;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Contexts;

public class UnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : AppDbContextBase
{
    private readonly TContext _context;
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<Type, IRepository>? _repositories;
    private bool _disposed;

    public UnitOfWork(TContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public TRepository GetRepository<TRepository>() where TRepository : IRepository
    {
        _repositories ??= new Dictionary<Type, IRepository>();

        var type = typeof(TRepository);

        if (_repositories.TryGetValue(type, out var repository))
            return (TRepository)repository;
            
        var serviceRepository = _serviceProvider.GetService<TRepository>();
        if (serviceRepository == null)
            throw CoreException.Exception($"Repository of type {type.Name} not found.");
            
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
        
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
        
    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _repositories?.Clear();
            _context.Dispose();
        }
        _disposed = true;
    }
}