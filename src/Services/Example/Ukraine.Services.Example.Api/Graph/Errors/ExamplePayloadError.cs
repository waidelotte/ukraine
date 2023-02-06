using FluentValidation;
using Ukraine.Services.Example.Domain.Exceptions;

namespace Ukraine.Services.Example.Api.Graph.Errors;

public class ExamplePayloadError
{
	public string Message { get; }

	public ExamplePayloadError(string message)
	{
		Message = message;
	}
	
	public static ExamplePayloadError CreateErrorFrom(ValidationException ex)
	{
		return new ExamplePayloadError(ex.Errors.First().ErrorMessage);
	}

	public static ExamplePayloadError CreateErrorFrom(ExampleException ex)
	{
		return new ExamplePayloadError(ex.Message);
	}
}