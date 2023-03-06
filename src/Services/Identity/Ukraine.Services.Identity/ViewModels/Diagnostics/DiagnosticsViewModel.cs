using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace Ukraine.Services.Identity.ViewModels.Diagnostics;

public class DiagnosticsViewModel
{
	public DiagnosticsViewModel(AuthenticateResult result)
	{
		AuthenticateResult = result;

		if (result.Properties?.Items.ContainsKey("client_list") == true)
		{
			var value = Encoding.UTF8.GetString(
				Base64Url.Decode(
					result.Properties.Items["client_list"]));

			Clients = JsonConvert.DeserializeObject<string[]>(value) ?? Array.Empty<string>();
		}
	}

	public AuthenticateResult AuthenticateResult { get; }

	public IEnumerable<string> Clients { get; } = new List<string>();
}