using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Domain.Interfaces;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;
using Ukraine.Services.Example.Infrastructure.UseCases.ReadAuthor;

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

	[HttpPost("CreateAuthor")]
	[ProducesResponseType(typeof(Author), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> CreateAuthorAsync(CreateAuthorRequest request)
	{
		_logger.LogDebug($"{nameof(CreateAuthorAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(CreateAuthorAsync)} controller end");

		return Ok(result);
	}

	[HttpPost("CreateBook")]
	[ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> CreateBookAsync(CreateBookRequest request)
	{
		_logger.LogDebug($"{nameof(CreateBookAsync)} controller start");

		var result = await _mediator.Send(request);

		_logger.LogDebug($"{nameof(CreateBookAsync)} controller end");

		return Ok(result);
	}

	[HttpGet("GetAuthors")]
	[ProducesResponseType(typeof(IEnumerable<Author>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult> GetAuthorsAsync()
	{
		_logger.LogDebug($"{nameof(GetAuthorsAsync)} controller start");

		var result = await _mediator.Send(new GetAuthorsQueryRequest());

		_logger.LogDebug($"{nameof(GetAuthorsAsync)} controller end");

		return Ok(result);
	}
}