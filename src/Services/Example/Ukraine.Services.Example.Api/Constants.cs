namespace Ukraine.Services.Example.Api;

internal static class Constants
{
	public const string SERVICE_ID = "service-example-api";
	public const string PUBSUB_NAME = "ukraine-pubsub";

	public static class Policy
	{
		public const string REST_API = "RestApi";
		public const string GRAPHQL_API = "GraphQlApi";
	}

	public static class Scope
	{
		public const string REST_SCOPE = "API_REST_EXAMPLE_SCOPE";
		public const string GRAPHQL_SCOPE = "API_GRAPHQL_EXAMPLE_SCOPE";
	}

	public static class ConfigurationSection
	{
		public const string AUTHENTICATION = "Authentication";
		public const string GRAPHQL = "GraphQl";
	}
}