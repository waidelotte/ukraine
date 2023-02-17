namespace Ukraine.Presentation.Swagger;

public static class Constants
{
	public const string ENDPOINT = $"/swagger/{DEFAULT_VERSION}/swagger.json";
	public const string DEFAULT_VERSION = "v1";
	public const string DEFAULT_ENDPOINT_NAME = "API";
	public const string DEFAULT_TITLE = "Ukraine Service API";
	public const string IDENTITY_AUTHORIZATION_URL = "/connect/authorize";
	public const string IDENTITY_TOKEN_URL = "/connect/token";
}