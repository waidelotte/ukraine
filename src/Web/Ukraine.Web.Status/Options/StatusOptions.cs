namespace Ukraine.Web.Status.Options;

public class StatusOptions
{
    public string ApplicationName { get; set; } = string.Empty;
    public string UIPath { get; set; } = string.Empty;
    public string ResourcesPath { get; set; } = string.Empty;
    public int EvaluationTimeInSeconds { get; set; }
    public int MaxActiveRequests { get; set; }
    public int MaximumHistoryEntriesPerEndpoint { get; set; }
}