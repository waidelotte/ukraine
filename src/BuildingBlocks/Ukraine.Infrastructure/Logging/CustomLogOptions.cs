namespace Ukraine.Infrastructure.Logging;

public class CustomLogOptions
{
    public string? ApplicationName { get; set; }
    public bool WriteToConsole { get; set; } = true;
    public bool WriteToSeq { get; set; } = false;
    public string? SeqServerUrl { get; set; }
    public bool UseSerilog { get; set; } = true;
}