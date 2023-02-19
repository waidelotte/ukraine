using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Ukraine.Presentation.Swagger.Options;

public class UkraineSwaggerWebOptionsBuilder
{
	private Action<SwaggerUIOptions>? _action;

	public void AddEndpoint(string url = Constants.ENDPOINT, string name = Constants.DEFAULT_ENDPOINT_NAME)
	{
		_action += options => options.SwaggerEndpoint(url, name);
	}

	public void AddOAuthClientId(string value)
	{
		_action += options => options.OAuthClientId(value);
	}

	public void AddOAuthAppName(string value)
	{
		_action += options => options.OAuthAppName(value);
	}

	internal Action<SwaggerUIOptions>? Build()
	{
		return _action;
	}
}