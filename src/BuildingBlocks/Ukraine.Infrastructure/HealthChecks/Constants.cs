namespace Ukraine.Infrastructure.HealthChecks;

public static class Constants
{
	public const string DEFAULT_SERVICE_NAME = "service";

	public static class Tags
	{
		public const string READY = "ready";
		public const string DATABASE = "database";
		public const string POSTGRES = "postgres";
	}

	public static class Endpoints
	{
		public const string READY = "/health/ready";
		public const string DATABASE = "/health/db";
	}
}