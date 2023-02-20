namespace Ukraine.Web.Status.Options;

public class WebStatusOptions
{
	public int EvaluationTimeInSeconds { get; set; } = 30;

	public int MaxActiveRequests { get; set; } = 1;

	public int MaximumHistoryEntriesPerEndpoint { get; set; } = 10;
}