using Microsoft.AspNetCore.Builder;

namespace Ukraine.Infrastructure.Identity.Extenstion;

public static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseUkraineAuthentication(this IApplicationBuilder applicationBuilder)
	{
		return applicationBuilder.UseAuthentication();
	}

	public static IApplicationBuilder UseUkraineAuthorization(this IApplicationBuilder applicationBuilder)
	{
		return applicationBuilder.UseAuthorization();
	}
}