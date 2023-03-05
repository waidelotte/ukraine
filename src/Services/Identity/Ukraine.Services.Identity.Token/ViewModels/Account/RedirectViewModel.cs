namespace Ukraine.Services.Identity.Token.ViewModels.Account;

public class RedirectViewModel
{
	public RedirectViewModel(string redirectUrl)
	{
		RedirectUrl = redirectUrl;
	}

	public string RedirectUrl { get; }
}