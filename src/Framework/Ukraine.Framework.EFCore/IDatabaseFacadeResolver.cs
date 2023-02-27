using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ukraine.Framework.EFCore;

public interface IDatabaseFacadeResolver
{
	DatabaseFacade Database { get; }
}