namespace Ukraine.Services.Example.Api;

internal static class Constants
{
	public const string SERVICE_ID = "service-example-api";

	public static class Policy
	{
		public const string REST_API = "RestApi";
		public const string GRAPHQL_API = "GraphQlApi";
		public const string GRAPHQL_ADMIN = "GraphQlGodMode";
	}

	public static class Scope
	{
		public const string REST_SCOPE = "service-example-rest-scope";
		public const string GRAPHQL_SCOPE = "service-example-graphql-scope";
	}

	public static class Role
	{
		public const string ADMIN = "admin";
	}
}