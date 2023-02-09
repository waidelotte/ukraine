using FluentValidation;
using Ukraine.Services.Example.Domain.Exceptions;

namespace Ukraine.Services.Example.Api.Graph.Errors;

public class PayloadError
{
	public PayloadError(string message)
	{
		Message = message;
	}

	public string Message { get; }

	public static PayloadError CreateErrorFrom(ValidationException ex)
	{
		return new PayloadError(ex.Errors.First().ErrorMessage);
	}

	public static PayloadError CreateErrorFrom(ExampleException ex)
	{
		return new PayloadError(ex.Message);
	}
}