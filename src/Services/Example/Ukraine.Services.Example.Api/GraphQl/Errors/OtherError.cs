namespace Ukraine.Services.Example.Api.GraphQl.Errors;

internal sealed class OtherError
{
	public OtherError(string message)
	{
		Message = message;
	}

	public string Message { get; }

	public static OtherError CreateErrorFrom(Exception ex)
	{
		return new OtherError(ex.Message);
	}
}