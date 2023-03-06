using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.ViewModels.Diagnostics;

namespace Ukraine.Services.Identity.Controllers;

[Authorize]
public class DiagnosticsController : Controller
{
	public async Task<IActionResult> Index()
	{
		var localAddresses = new[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress?.ToString() };

		if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress?.ToString()))
			return NotFound();

		var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
		return View(model);
	}
}