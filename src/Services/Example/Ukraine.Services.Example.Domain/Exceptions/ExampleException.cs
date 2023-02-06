namespace Ukraine.Services.Example.Domain.Exceptions;

public class ExampleException : Exception
{
	public ExampleException() { }
	
	public ExampleException(string message) : base(message) { }

	public static ExampleException Exception(string message)
	{
		return new ExampleException(message);
	}
}