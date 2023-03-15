using Dapr;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Framework.Abstractions;
using Ukraine.Services.Example.Friends.Email.Events;

namespace Ukraine.Services.Example.Friends.Email.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class AuthorSubscriberController : ControllerBase
{
	[HttpPost("AuthorRegistrationApproved")]
	[Topic("ukraine-pubsub", nameof(AuthorRegistrationApprovedEvent))]
	public async Task HandleAsync([FromServices] IEmailService emailService, AuthorRegistrationApprovedEvent request)
	{
		await emailService.SendEmailAsync(
			"Your registration has been completed successfully!",
			"test@test.com",
			"Registration is completed!");
	}
}