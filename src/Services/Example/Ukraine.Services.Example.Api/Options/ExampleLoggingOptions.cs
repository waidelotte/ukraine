using Serilog.Events;

namespace Ukraine.Services.Example.Api.Options;

public class ExampleLoggingOptions
{
	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
	public Dictionary<string, LogEventLevel>? Override { get; set; }
	public bool WriteToConsole { get; set; }
	public string? WriteToSeqServerUrl { get; set; }
}