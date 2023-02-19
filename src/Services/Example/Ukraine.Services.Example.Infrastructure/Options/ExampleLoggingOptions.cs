using Serilog.Events;

namespace Ukraine.Services.Example.Infrastructure.Options;

public class ExampleLoggingOptions
{
	public const string SECTION_NAME = "CustomLogging";

	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

	public Dictionary<string, LogEventLevel>? Override { get; set; }

	public bool WriteToConsole { get; set; }

	public string? WriteToSeqServerUrl { get; set; }
}