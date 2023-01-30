namespace Ukraine.Infrastructure.Logging.Options;

public class CustomLogOptions
{
    public string? ApplicationName { get; set; }
    public bool WriteToConsole { get; set; } = true;
    public bool WriteToSeq { get; set; }
    public string? SeqServerUrl { get; set; }
    public bool UseSerilog { get; set; } = true;
}