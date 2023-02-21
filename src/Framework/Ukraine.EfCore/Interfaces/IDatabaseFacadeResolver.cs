using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ukraine.EfCore.Interfaces;

public interface IDatabaseFacadeResolver
{
	DatabaseFacade Database { get; }
}