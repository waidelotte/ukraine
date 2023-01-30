namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleLoggingOptions
{
	public const string SectionName = "CustomLogging";
	
	public bool WriteToConsole { get; set; }
	public bool WriteToSeq { get; set; }
	public string? SeqServerUrl { get; set; }
	public bool UseSerilog { get; set; }
}