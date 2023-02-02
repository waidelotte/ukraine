using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukraine.Domain.Abstractions;
using Ukraine.Domain.Exceptions;
using Ukraine.Infrastructure.EfCore.Interfaces;

namespace Ukraine.Infrastructure.EfCore.Contexts;

public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>, IDisposable where TDbContext : DbContext
{
    private readonly TDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private Dictionary<Type, IRepository>? _repositories;
    private bool _disposed;

    public UnitOfWork(TDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }
    
    public IRepository<TDbContext, TEntity> GetRepository<TEntity>() where TEntity : class, IAggregateRoot
    {
        _repositories ??= new Dictionary<Type, IRepository>();

        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repository))
            return (IRepository<TDbContext, TEntity>)repository;
            
        var serviceRepository = _serviceProvider.GetService<IRepository<TDbContext, TEntity>>();
        if (serviceRepository == null)
            throw CoreException.Exception($"Repository of type {typeof(TEntity)} not found.");
            
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