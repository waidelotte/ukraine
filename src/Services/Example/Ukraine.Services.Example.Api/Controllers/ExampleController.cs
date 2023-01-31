using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Infrastructure.UseCases;
using Ukraine.Services.Example.Infrastructure.UseCases.Create;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly DaprClient _daprClient;
	private readonly ILogger<ExampleController> _logger;

	public ExampleController(IMediator mediator, DaprClient daprClient, ILogger<ExampleController> logger)
	{
		_mediator = mediator;
		_daprClient = daprClient;
		_logger = logger;
	}

	[HttpGet("GetOk")]
	public async Task<ActionResult> GetOkAsync(CancellationToken cancellationToken)
	{
		_logger.LogDebug($"{nameof(GetOkAsync)} controller start");

		var emptyEvent = new ExampleEmptyEvent();
		await _daprClient.PublishEventAsync("ukraine-pubsub", emptyEvent.GetType().Name, emptyEvent, cancellationToken);

		_logger.LogDebug($"{nameof(GetOkAsync)} controller end");
		return Ok();
	}
	
	[HttpPost("CreateExampleEntity")]
	public async Task<ActionResult> CreateExampleEntityAsync(CreateExampleEntityRequest request)
	{
		_logger.LogDebug($"{nameof(CreateExampleEntityAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(CreateExampleEntityAsync)} controller end");
		
		return Ok(result);
	}
}