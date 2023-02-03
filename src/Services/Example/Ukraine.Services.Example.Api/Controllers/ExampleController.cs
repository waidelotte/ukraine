using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Domain.Abstractions;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleChildEntity;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateExampleEntity;
using Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

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

		var emptyEvent = new ExampleEmptyEvent();
		await _eventBus.PublishAsync(emptyEvent, cancellationToken);

		_logger.LogDebug($"{nameof(GetOkAsync)} controller end");
		return Ok();
	}
	
	[HttpPost("CreateExampleEntity")]
	[ProducesResponseType(typeof(CreateExampleEntityResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> CreateExampleEntityAsync(CreateExampleEntityRequest request)
	{
		_logger.LogDebug($"{nameof(CreateExampleEntityAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(CreateExampleEntityAsync)} controller end");
		
		return Ok(result);
	}
	
	[HttpPost("CreateChildExampleEntity")]
	[ProducesResponseType(typeof(CreateExampleChildEntityResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> CreateExampleChildEntityAsync(CreateExampleChildEntityRequest request)
	{
		_logger.LogDebug($"{nameof(CreateExampleChildEntityAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(CreateExampleChildEntityAsync)} controller end");
		
		return Ok(result);
	}
	
	[HttpGet("GetExampleEntities")]
	[ProducesResponseType(typeof(GetExampleEntitiesResponse), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> GetExampleEntitiesAsync([FromQuery] GetExampleEntitiesRequest request)
	{
		_logger.LogDebug($"{nameof(GetExampleEntitiesAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(GetExampleEntitiesAsync)} controller end");
		
		return Ok(result);
	}
}