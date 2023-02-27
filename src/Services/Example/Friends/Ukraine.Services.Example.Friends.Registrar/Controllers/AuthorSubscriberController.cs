using Dapr;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Framework.Abstractions;
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

	[HttpPost("AuthorRegistered")]
	[Topic("ukraine-pubsub", nameof(AuthorRegisteredEvent))]
	public async Task HandleAsync(AuthorRegisteredEvent request)
	{
		_logger.LogDebug("Author Registered Event: {@Request}", request);
		_logger.LogDebug("Register new Author {Id}", request.AuthorId);

		await Task.Delay(TimeSpan.FromSeconds(2)); // some work

		var gen = new Random();

		if (gen.Next(100) < 50)
		{
			await _eventBus.PublishAsync(new AuthorRegistrationApprovedEvent(request.AuthorId));
		}
		else
		{
			await _eventBus.PublishAsync(new AuthorRegistrationDeclinedEvent(request.AuthorId));
		}
	}
}