using Serilog.Events;

namespace Ukraine.Services.Identity.Options;

public class IdentityLoggingOptions
{
	public const string SECTION_NAME = "LoggingOptions";

	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

	public Dictionary<string, LogEventLevel> Override { get; set; } = new();

	public bool WriteToConsole { get; set; }

	public string? WriteToSeqServerUrl { get; set; }
}