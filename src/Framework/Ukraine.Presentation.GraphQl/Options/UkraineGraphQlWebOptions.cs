namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlWebOptions
{
	public string Path { get; set; } = Constants.DEFAULT_PATH;

	public bool EnableSchemaRequests { get; set; }

	public bool EnableGetRequests { get; set; }

	public bool EnableMultipartRequests { get; set; }

	public bool EnableBananaCakePop { get; set; }

	public bool EnableBatching { get; set; }
}