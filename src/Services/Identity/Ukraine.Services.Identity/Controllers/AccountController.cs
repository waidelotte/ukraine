using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.Persistence.Entities;
using Ukraine.Services.Identity.ViewModels.Account;

namespace Ukraine.Services.Identity.Controllers;

[Authorize]
public class AccountController : Controller
{
	private readonly UserManager<UserIdentity> _userManager;
	private readonly SignInManager<UserIdentity> _signInManager;
	private readonly IIdentityServerInteractionService _interaction;

	public AccountController(
		UserManager<UserIdentity> userManager,
		SignInManager<UserIdentity> signInManager,
		IIdentityServerInteractionService interaction)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_interaction = interaction;
	}

	[HttpGet]
	[AllowAnonymous]
	public IActionResult Login(string? returnUrl)
	{
		var vm = new LoginInputViewModel
		{
			ReturnUrl = returnUrl
		};

		return View(vm);
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginInputViewModel model, string button)
	{
		var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

		// the user clicked the "cancel" button
		if (button != "login")
		{
			if (context != null && !string.IsNullOrEmpty(model.ReturnUrl))
			{
				await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

				return Redirect(model.ReturnUrl);
			}

			return Redirect("~/");
		}

		if (ModelState.IsValid)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user != default(UserIdentity))
			{
				var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberLogin, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					if (context != null && !string.IsNullOrEmpty(model.ReturnUrl))
						Redirect(model.ReturnUrl);

					if (Url.IsLocalUrl(model.ReturnUrl))
						return Redirect(model.ReturnUrl);

					if (string.IsNullOrEmpty(model.ReturnUrl))
						return Redirect("~/");

					throw new Exception("Invalid return URL");
				}

				if (result.IsLockedOut)
					return View("Lockout");
			}

			ModelState.AddModelError(string.Empty, "Invalid username or password");
		}

		var vm = new LoginInputViewModel
		{
			ReturnUrl = model.ReturnUrl,
			Username = model.Username,
			RememberLogin = model.RememberLogin
		};

		return View(vm);
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Logout(string logoutId)
	{
		return await Logout(new LogoutInputViewModel
		{
			LogoutId = logoutId
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout(LogoutInputViewModel model)
	{
		var logout = await _interaction.GetLogoutContextAsync(model.LogoutId);

		if (User.Identity?.IsAuthenticated == true)
			await _signInManager.SignOutAsync();

		return Redirect(string.IsNullOrEmpty(logout?.PostLogoutRedirectUri) ? "~/" : logout.PostLogoutRedirectUri);
	}
}