namespace Ukraine.Presentation.GraphQl;

internal static class Constants
{
	public const string DEFAULT_PATH = "/graphql";

	public static class Paging
	{
		public const int MAX_PAGE_SIZE = 100;
		public const int DEFAULT_PAGE_SIZE = 10;
		public const bool INCLUDE_TOTAL_COUNT = true;
		public const bool ALLOW_BACKWARD = false;
	}
}