using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace Ukraine.Services.Example.Api.GraphQl.Interceptors;

public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
{
	public override ValueTask OnCreateAsync(
		HttpContext context,
		IRequestExecutor requestExecutor,
		IQueryRequestBuilder requestBuilder,
		CancellationToken cancellationToken)
	{
		var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		requestBuilder.SetGlobalState("UserId", userId);

		return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
	}
}