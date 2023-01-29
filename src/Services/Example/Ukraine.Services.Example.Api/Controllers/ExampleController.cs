using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Events;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
	private readonly DaprClient _daprClient;
	private readonly ILogger<ExampleController> _logger;

	public ExampleController(DaprClient daprClient, ILogger<ExampleController> logger)
	{
		_daprClient = daprClient;
		_logger = logger;
	}

	[HttpGet(Name = "ExampleGet")]
	public async Task<ActionResult> ExampleGet(CancellationToken cancellationToken)
	{
		_logger.LogDebug("ExampleGet controller start");

		var emptyEvent = new ExampleEmptyEvent();
		await _daprClient.PublishEventAsync("ukraine-pubsub", emptyEvent.GetType().Name, emptyEvent, cancellationToken);

		_logger.LogDebug("ExampleGet controller end");
		return Ok();
	}
}