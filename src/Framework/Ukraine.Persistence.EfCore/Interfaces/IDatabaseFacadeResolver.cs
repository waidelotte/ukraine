using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ukraine.Persistence.EfCore.Interfaces;

public interface IDatabaseFacadeResolver
{
	DatabaseFacade Database { get; }
}