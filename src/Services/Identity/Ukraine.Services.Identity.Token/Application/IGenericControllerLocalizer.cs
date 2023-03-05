using Microsoft.Extensions.Localization;

namespace Ukraine.Services.Identity.Token.Application
{
	public interface IGenericControllerLocalizer<out T>
	{
		LocalizedString this[string name] { get; }

		LocalizedString this[string name, params object[] arguments] { get; }
	}
}