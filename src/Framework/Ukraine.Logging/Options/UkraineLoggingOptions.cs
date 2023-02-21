using System.ComponentModel.DataAnnotations;
using Serilog.Events;

namespace Ukraine.Logging.Options;

public class UkraineLoggingOptions
{
	[Required]
	public required string ServiceName { get; set; }

	public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

	public Dictionary<string, LogEventLevel> Override { get; set; } = new();

	public UkraineLoggingWriteOptions WriteTo { get; set; } = new();
}