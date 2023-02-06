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
	
	[HttpPost("Empty")]
	[Dapr.Topic(Constants.PUB_SUB_NAME, nameof(EmptyEvent))]
	public async Task HandleAsync(EmptyEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
	}
	
	[HttpPost("AuthorCreated")]
	[Dapr.Topic(Constants.PUB_SUB_NAME, nameof(AuthorCreatedEvent))]
	public async Task HandleAsync(AuthorCreatedEvent request)
	{
		_logger.LogDebug("Subscriber Event: {@Request}", request);
	}
}