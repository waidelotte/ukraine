using Serilog.Events;

namespace Ukraine.Services.Example.Friends.Registrar.Options;

public class RegistrarLoggingOptions
{
	public const string SECTION_NAME = "LoggingOptions";

	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

	public Dictionary<string, LogEventLevel>? Override { get; set; }

	public bool WriteToConsole { get; set; }

	public string? WriteToSeqServerUrl { get; set; }
}