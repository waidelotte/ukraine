using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ukraine.Framework.Abstractions;

namespace Ukraine.Framework.Dapr;

internal class DaprEmailService : IEmailService
{
	private readonly IOptions<DaprEmailOptions> _options;
	private readonly DaprClient _daprClient;
	private readonly ILogger<DaprEmailService> _logger;

	public DaprEmailService(IOptions<DaprEmailOptions> options, DaprClient daprClient, ILogger<DaprEmailService> logger)
	{
		_options = options;
		_daprClient = daprClient;
		_logger = logger;
	}

	public async Task SendEmailAsync(object data, string emailTo, string subject)
	{
		_logger.LogDebug("Sending Email to {EmailTo}: {@Data}", emailTo, data);

		await _daprClient.InvokeBindingAsync(
			_options.Value.BindingName,
			"create",
			data,
			new Dictionary<string, string>
			{
				["emailFrom"] = _options.Value.EmailFrom,
				["emailTo"] = emailTo,
				["subject"] = subject
			});
	}
}