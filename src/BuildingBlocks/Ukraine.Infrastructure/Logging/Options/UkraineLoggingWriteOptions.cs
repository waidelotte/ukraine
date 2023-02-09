namespace Ukraine.Infrastructure.Logging.Options;

public class UkraineLoggingWriteOptions
{
	public string? WriteToSeqServerUrl { get; set; }

	public bool WriteToConsole { get; set; } = true;
}