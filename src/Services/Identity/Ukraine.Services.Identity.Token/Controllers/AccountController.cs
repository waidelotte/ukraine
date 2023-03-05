using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.Token.Application;
using Ukraine.Services.Identity.Token.ViewModels.Account;

namespace Ukraine.Services.Identity.Token.Controllers;

[SecurityHeaders]
[Authorize]
public class AccountController<TUser, TKey> : Controller
	where TUser : IdentityUser<TKey>, new()
	where TKey : IEquatable<TKey>
{
	private readonly UserManager<TUser> _userManager;
	private readonly SignInManager<TUser> _signInManager;
	private readonly IIdentityServerInteractionService _interaction;
	private readonly IEventService _events;

	public AccountController(
		UserManager<TUser> userManager,
		SignInManager<TUser> signInManager,
		IIdentityServerInteractionService interaction,
		IEventService events)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_interaction = interaction;
		_events = events;
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

				return context.IsNativeClient() ? this.LoadingPage("Redirect", model.ReturnUrl) : Redirect(model.ReturnUrl);
			}

			return Redirect("~/");
		}

		if (ModelState.IsValid)
		{
			var user = await _userManager.FindByNameAsync(model.Username);
			if (user != default(TUser))
			{
				var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberLogin, lockoutOnFailure: true);
				if (result.Succeeded)
				{
					await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));

					if (context != null && !string.IsNullOrEmpty(model.ReturnUrl))
					{
						return context.IsNativeClient() ? this.LoadingPage("Redirect", model.ReturnUrl) : Redirect(model.ReturnUrl);
					}

					if (Url.IsLocalUrl(model.ReturnUrl))
						return Redirect(model.ReturnUrl);

					if (string.IsNullOrEmpty(model.ReturnUrl))
						return Redirect("~/");

					throw new Exception("Invalid return URL");
				}

				if (result.IsLockedOut)
					return View("Lockout");
			}

			await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "Invalid credentials", clientId: context?.Client.ClientId));
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

		var vm = new LoggedOutViewModel
		{
			PostLogoutRedirectUri = logout?.PostLogoutRedirectUri
		};

		if (User.Identity?.IsAuthenticated == true)
		{
			await _signInManager.SignOutAsync();
			await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
		}

		return View("LoggedOut", vm);
	}
}