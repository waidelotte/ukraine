using Dapr;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Infrastructure;
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
	
	[HttpPost("ExampleEmpty")]
	[Topic(Constants.PUB_SUB_NAME, nameof(ExampleEmptyEvent))]
	public async Task HandleAsync(ExampleEmptyEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
	}
	
	[HttpPost("ExampleEntityCreated")]
	[Topic(Constants.PUB_SUB_NAME, nameof(ExampleEntityCreatedEvent))]
	public async Task HandleAsync(ExampleEntityCreatedEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
	}
}