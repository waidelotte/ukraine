using Microsoft.AspNetCore.Mvc;
using Ukraine.Domain.Interfaces;
using Ukraine.Services.Example.Friends.Registrar.Events;

namespace Ukraine.Services.Example.Friends.Registrar.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class AuthorSubscriberController : ControllerBase
{
	private readonly ILogger<AuthorSubscriberController> _logger;
	private readonly IEventBus _eventBus;

	public AuthorSubscriberController(ILogger<AuthorSubscriberController> logger, IEventBus eventBus)
	{
		_logger = logger;
		_eventBus = eventBus;
	}

	[HttpPost("AuthorCreated")]
	[Dapr.Topic(Infrastructure.EventBus.Dapr.Constants.PUB_SUB_NAME, nameof(AuthorCreatedEvent))]
	public async Task HandleAsync(AuthorCreatedEvent request)
	{
		_logger.LogDebug("Author Created Event: {@Request}", request);
		_logger.LogDebug("Register new Author {Id}", request.AuthorId);
		await _eventBus.PublishAsync(new AuthorRegisteredEvent(request.AuthorId));
	}
}