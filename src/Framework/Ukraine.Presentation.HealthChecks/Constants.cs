namespace Ukraine.Presentation.HealthChecks;

internal static class Constants
{
	public static class Tags
	{
		public const string READY = "ready";
		public const string SERVICE = "service";
		public const string DATABASE = "database";
		public const string POSTGRES = "postgres";
	}

	public static class Endpoints
	{
		public const string READY = "/health/ready";
		public const string READY_DATABASE = "/health/ready/db";
	}
}