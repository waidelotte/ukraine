namespace Ukraine.Services.Identity.Exceptions;

internal sealed class IdentityException : Exception
{
	private IdentityException(string message) : base(message) { }

	public static IdentityException Exception(string message)
	{
		return new IdentityException(message);
	}
}