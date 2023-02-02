using Microsoft.EntityFrameworkCore;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Domain.Repositories;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Repositories;

public class ExampleEntityRepository : IExampleEntityRepository
{
	private readonly ExampleContext _context;

	public ExampleEntityRepository(ExampleContext context)
	{
		_context = context;
	}

	public async Task<ExampleEntity> AddAsync(ExampleEntity entity, CancellationToken cancellationToken = default)
	{
		var entry = await _context.ExampleEntities.AddAsync(entity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return entry.Entity;
	}

	public async Task<IEnumerable<ExampleEntity>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
	{
		return await _context.ExampleEntities
			.AsNoTracking()
			.Skip((pageIndex - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);
	}

	public async Task<int> CountAsync(CancellationToken cancellationToken = default)
	{
		return await _context.ExampleEntities.CountAsync(cancellationToken);
	}
}