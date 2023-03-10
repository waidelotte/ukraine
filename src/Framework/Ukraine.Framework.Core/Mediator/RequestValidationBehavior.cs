using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ukraine.Framework.Core.Mediator;

internal sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

	public RequestValidationBehavior(
		IEnumerable<IValidator<TRequest>> validators,
		ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
	{
		_validators = validators;
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		_logger.LogDebug($"Start validation for {nameof(request)}");

		if (_validators.Any())
		{
			var context = new ValidationContext<TRequest>(request);
			var validationResults =
				await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

			var failures = validationResults
				.SelectMany(x => x.Errors)
				.Where(f => f != null)
				.ToList();

			Guard.Against.Failure(failures);
		}

		_logger.LogDebug($"The {nameof(request)} is valid.");

		return await next();
	}
}