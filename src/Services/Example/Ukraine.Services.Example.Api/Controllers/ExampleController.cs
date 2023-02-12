using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Domain.Interfaces;
using Ukraine.Services.Example.Domain.Events;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IEventBus _eventBus;
	private readonly ILogger<ExampleController> _logger;

	public ExampleController(IMediator mediator, IEventBus eventBus, ILogger<ExampleController> logger)
	{
		_mediator = mediator;
		_eventBus = eventBus;
		_logger = logger;
	}

	[HttpGet("GetOk")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<ActionResult> GetOkAsync(CancellationToken cancellationToken)
	{
		_logger.LogDebug($"{nameof(GetOkAsync)} controller start");

		var emptyEvent = new EmptyEvent();
		await _eventBus.PublishAsync(emptyEvent, cancellationToken);

		_logger.LogDebug($"{nameof(GetOkAsync)} controller end");
		return Ok();
	}

	[HttpGet("SeedData")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<ActionResult> SeedDataAsync(CancellationToken cancellationToken)
	{
		_logger.LogDebug($"{nameof(SeedDataAsync)} controller start");

		var emptyEvent = new EmptyEvent();
		await _eventBus.PublishAsync(emptyEvent, cancellationToken);

		_logger.LogDebug($"{nameof(SeedDataAsync)} controller end");
		return Ok();
	}
}