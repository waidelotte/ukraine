using Microsoft.EntityFrameworkCore;
using Ukraine.Persistence.EfCore.Interceptors;

namespace Ukraine.Persistence.EfCore.Extensions;

public static class DbContextOptionsBuilderExtensions
{
	public static DbContextOptionsBuilder AddAudit(this DbContextOptionsBuilder builder)
	{
		return builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
	}
}