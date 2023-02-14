using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Domain.Enums;
using Ukraine.Services.Example.Domain.Events;
using Ukraine.Services.Example.Infrastructure.UseCases.ChangeAuthorStatus;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class AuthorSubscriberController : ControllerBase
{
	private readonly ILogger<AuthorSubscriberController> _logger;
	private readonly IMediator _mediator;

	public AuthorSubscriberController(ILogger<AuthorSubscriberController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpPost("AuthorRegistered")]
	[Dapr.Topic(Ukraine.Infrastructure.EventBus.Dapr.Constants.PUB_SUB_NAME, nameof(AuthorRegisteredEvent))]
	public async Task HandleAsync(AuthorRegisteredEvent request)
	{
		_logger.LogDebug("AuthorRegisteredEvent: {@Request}", request);
		await _mediator.Send(new ChangeAuthorStatusRequest(request.AuthorId, AuthorStatus.Registered));
	}
}