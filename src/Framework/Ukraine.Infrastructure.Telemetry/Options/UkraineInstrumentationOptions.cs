namespace Ukraine.Infrastructure.Telemetry.Options;

public class UkraineInstrumentationOptions
{
	public bool AspNetCore { get; set; } = true;

	public bool HttpClient { get; set; } = true;

	public bool SqlClient { get; set; }

	public bool HotChocolate { get; set; }
}