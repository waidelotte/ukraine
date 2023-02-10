using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Infrastructure.State;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class ExampleSubscriberController : ControllerBase
{
	private readonly ILogger<ExampleSubscriberController> _logger;
	private readonly DaprClient _daprClient;

	public ExampleSubscriberController(ILogger<ExampleSubscriberController> logger, DaprClient daprClient)
	{
		_logger = logger;
		_daprClient = daprClient;
	}

	[HttpPost("Empty")]
	[Dapr.Topic(Ukraine.Infrastructure.EventBus.Dapr.Constants.PUB_SUB_NAME, nameof(EmptyEvent))]
	public async Task HandleAsync(EmptyEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
	}

	[HttpPost("AuthorCreated")]
	[Dapr.Topic(Ukraine.Infrastructure.EventBus.Dapr.Constants.PUB_SUB_NAME, nameof(AuthorCreatedEvent))]
	public async Task HandleAsync(AuthorCreatedEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
		var state = await _daprClient.GetStateAsync<AuthorState>("ukraine-statestore", $"author-{request.AuthorId}");
		_logger.LogDebug("Author State: {@State}", state);
	}
}