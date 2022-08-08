using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ukraine.Infrastructure.EfCore.Interfaces
{
    public interface IDatabaseFacadeResolver
    {
        DatabaseFacade Database { get; }
    }
}