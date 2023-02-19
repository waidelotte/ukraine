namespace Ukraine.Infrastructure.Logging.Options;

public class UkraineLoggingWriteOptions
{
	public string? SeqServerUrl { get; set; }

	public bool Console { get; set; } = true;
}