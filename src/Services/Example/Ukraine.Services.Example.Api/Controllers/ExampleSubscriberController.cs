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
	
	[HttpPost]
	[Topic(Constants.PUB_SUB_NAME, nameof(ExampleEmptyEvent))]
	public IActionResult ExampleEmptyEvent([FromBody] ExampleEmptyEvent request)
	{
		_logger.LogDebug("[Created: {Date}][ID: {ID}] ExampleEmptyEvent request catched", request.CreatedAt, request.Id);
		return Ok();
	}
}