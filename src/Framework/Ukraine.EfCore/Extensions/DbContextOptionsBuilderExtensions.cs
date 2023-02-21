using Microsoft.EntityFrameworkCore;
using Ukraine.EfCore.Interceptors;

namespace Ukraine.EfCore.Extensions;

public static class DbContextOptionsBuilderExtensions
{
	public static DbContextOptionsBuilder AddAudit(this DbContextOptionsBuilder builder)
	{
		return builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
	}
}