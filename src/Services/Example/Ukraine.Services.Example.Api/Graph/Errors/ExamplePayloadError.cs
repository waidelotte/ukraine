using FluentValidation;

namespace Ukraine.Services.Example.Api.Graph.Errors;

public class ExamplePayloadError
{
	public string Message { get; }

	public ExamplePayloadError(ValidationException exception)
	{
		Message = exception.Errors.First().ErrorMessage;
	}
}