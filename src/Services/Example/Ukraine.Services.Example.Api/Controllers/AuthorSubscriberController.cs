using Dapr;
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
	private const string PUBSUB_NAME = "ukraine-pubsub";

	private readonly ILogger<AuthorSubscriberController> _logger;
	private readonly IMediator _mediator;

	public AuthorSubscriberController(ILogger<AuthorSubscriberController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpPost("AuthorRegistrationApproved")]
	[Topic(PUBSUB_NAME, nameof(AuthorRegistrationApprovedEvent))]
	public async Task HandleAsync(AuthorRegistrationApprovedEvent request)
	{
		_logger.LogDebug("AuthorRegistrationApprovedEvent: {@Request}", request);
		await _mediator.Send(new ChangeAuthorStatusRequest(request.AuthorId, AuthorStatus.Registered));
	}

	[HttpPost("AuthorRegistrationDeclined")]
	[Topic(PUBSUB_NAME, nameof(AuthorRegistrationDeclinedEvent))]
	public async Task HandleAsync(AuthorRegistrationDeclinedEvent request)
	{
		_logger.LogDebug("AuthorRegistrationDeclinedEvent: {@Request}", request);
		await _mediator.Send(new ChangeAuthorStatusRequest(request.AuthorId, AuthorStatus.RegistrationDeclined));
	}
}