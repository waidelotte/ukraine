namespace Ukraine.Web.Status.Options;

public class StatusOptions
{
	public string? ServiceName { get; set; }

	public string? UiPath { get; set; }

	public string? ResourcesPath { get; set; }

	public int EvaluationTimeInSeconds { get; set; }

	public int MaxActiveRequests { get; set; }

	public int MaximumHistoryEntriesPerEndpoint { get; set; }
}