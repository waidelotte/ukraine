using FluentValidation;

namespace Ukraine.Services.Example.Api.GraphQl.Errors;

internal sealed class ValidationError
{
	public ValidationError(string message, IEnumerable<string> errors)
	{
		Message = message;
		Errors = errors;
	}

	public string Message { get; }

	public IEnumerable<string> Errors { get; }

	public static ValidationError CreateErrorFrom(ValidationException ex)
	{
		return new ValidationError(ex.Message, ex.Errors.Select(e => e.ErrorMessage));
	}
}