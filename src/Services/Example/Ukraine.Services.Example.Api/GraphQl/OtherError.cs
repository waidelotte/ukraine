using Ukraine.Services.Example.Domain.Exceptions;

namespace Ukraine.Services.Example.Api.GraphQl;

public class OtherError
{
	public OtherError(string message)
	{
		Message = message;
	}

	public string Message { get; }

	public static OtherError CreateErrorFrom(ExampleException ex)
	{
		return new OtherError(ex.Message);
	}

	public static OtherError CreateErrorFrom(Exception ex)
	{
		return new OtherError(ex.Message);
	}
}