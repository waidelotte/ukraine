using Ukraine.Domain.Abstractions;
using Ukraine.Services.Example.Domain.Entities;

namespace Ukraine.Services.Example.Domain.Repositories;

public interface IExampleEntityRepository : IRepository
{
	Task<ExampleEntity> AddAsync(ExampleEntity entity, CancellationToken cancellationToken = default);
	Task<IEnumerable<ExampleEntity>> GetAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
	Task<int> CountAsync(CancellationToken cancellationToken = default);
}