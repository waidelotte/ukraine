namespace Ukraine.Services.Identity.Helpers;

public class CookieHelper
{
	public static void SetSameSite(HttpContext httpContext, CookieOptions options)
	{
		if (options.SameSite != SameSiteMode.None || httpContext.Request.IsHttps) return;
		options.SameSite = SameSiteMode.Unspecified;
	}
}