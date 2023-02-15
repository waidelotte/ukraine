using System.Net;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;
using Ukraine.Services.Example.Infrastructure.UseCases.CreateBook;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<ExampleController> _logger;

	public ExampleController(IMediator mediator, ILogger<ExampleController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	[HttpGet("GetOk")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public OkResult GetOk()
	{
		return Ok();
	}

	[HttpGet("SeedData")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<ActionResult> SeedDataAsync(CancellationToken cancellationToken)
	{
		_logger.LogDebug($"{nameof(SeedDataAsync)} controller start");

		var authorRequestFaker = new Faker<CreateAuthorRequest>()
			.CustomInstantiator(f => new CreateAuthorRequest(f.Name.FullName(), f.Random.Number(5, 90)));

		foreach (var request in authorRequestFaker.Generate(50))
		{
			var author = await _mediator.Send(request, cancellationToken);

			var bookRequestFaker = new Faker<CreateBookRequest>()
				.CustomInstantiator(f => new CreateBookRequest(author.Id, f.System.CommonFileName()));

			foreach (var createBookRequest in bookRequestFaker.Generate(3))
			{
				await _mediator.Send(createBookRequest, cancellationToken);
			}
		}

		_logger.LogDebug($"{nameof(SeedDataAsync)} controller end");
		return Ok();
	}
}