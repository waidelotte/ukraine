namespace Ukraine.Infrastructure.EventBus.Dapr;

public static class Constants
{
	public const string SERVICE_NAME = "dapr";
	public const string PUB_SUB_NAME = "ukraine-pubsub";
	
	public static class Serialization
	{
		public const bool CaseInsensitive = true;
	}
}