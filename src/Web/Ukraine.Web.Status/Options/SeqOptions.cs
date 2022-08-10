namespace Ukraine.Web.Status.Options;

public class SeqOptions
{
    public const string Position = "Seq";
    
    public bool IsEnabled { get; set; } = false;
    public string ServerUrl { get; set; } = string.Empty;
}