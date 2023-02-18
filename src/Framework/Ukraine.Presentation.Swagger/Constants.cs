namespace Ukraine.Presentation.Swagger;

internal static class Constants
{
	public const string ENDPOINT = $"/swagger/{DEFAULT_VERSION}/swagger.json";
	public const string DEFAULT_VERSION = "v1";
	public const string DEFAULT_ENDPOINT_NAME = "API";
	public const string DEFAULT_TITLE = "Ukraine Service API";
	public const string IDENTITY_AUTHORIZATION_URL = "/connect/authorize";
	public const string IDENTITY_TOKEN_URL = "/connect/token";

	public static class Status
	{
		public const string UNAUTHORIZED = "Unauthorized";
		public const string FORBIDDEN = "Forbidden";
	}
}