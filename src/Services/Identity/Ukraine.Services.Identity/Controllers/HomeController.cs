using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.ViewModels.Home;

namespace Ukraine.Services.Identity.Controllers
{
	public class HomeController : Controller
	{
		private readonly IIdentityServerInteractionService _interaction;

		public HomeController(IIdentityServerInteractionService interaction)
		{
			_interaction = interaction;
		}

		public async Task<IActionResult> Error(string errorId)
		{
			var vm = new ErrorViewModel();

			var message = await _interaction.GetErrorContextAsync(errorId);

			if (message != null)
				vm.Error = message;

			return View("Error", vm);
		}
	}
}