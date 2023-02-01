namespace Ukraine.Web.Status.Options;

public class SeqOptions
{
    public const string SectionName = "Seq";
    
    public bool IsEnabled { get; set; }
    public string? ServerUrl { get; set; }
}