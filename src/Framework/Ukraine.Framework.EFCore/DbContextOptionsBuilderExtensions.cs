using Microsoft.EntityFrameworkCore;

namespace Ukraine.Framework.EFCore;

public static class DbContextOptionsBuilderExtensions
{
	public static DbContextOptionsBuilder AddAuditSaveInterceptor(this DbContextOptionsBuilder builder)
	{
		return builder.AddInterceptors(new AuditEntitiesSaveInterceptor());
	}
}