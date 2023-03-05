using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.ViewModels.Account;

public class LoginInputViewModel
{
	[Required]
	public string? Username { get; set; }

	[Required]
	public string? Password { get; set; }

	public bool RememberLogin { get; set; }

	public string? ReturnUrl { get; set; }
}