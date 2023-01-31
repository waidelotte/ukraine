using Dapr;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Events;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class ExampleSubscriberController : ControllerBase
{
	private readonly ILogger<ExampleSubscriberController> _logger;

	public ExampleSubscriberController(ILogger<ExampleSubscriberController> logger)
	{
		_logger = logger;
	}
	[HttpPost]
	[Topic("ukraine-pubsub", "ExampleEmptyEvent")]
	public IActionResult ExampleEmptyEvent([FromBody] ExampleEmptyEvent request)
	{
		_logger.LogDebug("ExampleEmptyEvent request catched. Created: {CreatedAt}", request.CreatedAt);
		return Ok();
	}
}