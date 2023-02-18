using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ukraine.Services.Identity.Pages;

[AllowAnonymous]
public class Index : PageModel
{
	public string? Version { get; private set; }

	public void OnGet()
	{
		Version = typeof(Duende.IdentityServer.Hosting.IdentityServerMiddleware).Assembly
			.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+').FirstOrDefault();
	}
}