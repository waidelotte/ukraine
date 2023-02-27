using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ukraine.Services.Example.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Constants.Policy.REST_API)]
public class ExampleController : ControllerBase
{
	[AllowAnonymous]
	[HttpGet("GetOk")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public OkResult GetOk()
	{
		return Ok();
	}

	[HttpGet("GetOkWithAuth")]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public OkResult GetOkWithAuth()
	{
		return Ok();
	}
}