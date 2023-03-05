using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.Token.ViewModels.Account;

namespace Ukraine.Services.Identity.Token.Application;

public static class Extensions
{
	public static bool IsNativeClient(this AuthorizationRequest context)
	{
		return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
			   && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
	}

	public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
	{
		controller.HttpContext.Response.StatusCode = 200;
		controller.HttpContext.Response.Headers["Location"] = string.Empty;

		return controller.View(viewName, new RedirectViewModel(redirectUri));
	}
}