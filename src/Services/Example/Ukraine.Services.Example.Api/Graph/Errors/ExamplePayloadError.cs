using FluentValidation;

namespace Ukraine.Services.Example.Api.Graph.Errors;

public class ExamplePayloadError
{
	public List<string> Messages { get; } = new();
	public string Message { get; }

	public ExamplePayloadError(ValidationException exception)
	{
		Messages.AddRange(exception.Errors.Select(failure => failure.ErrorMessage));
		Message = exception.Message;
	}
}