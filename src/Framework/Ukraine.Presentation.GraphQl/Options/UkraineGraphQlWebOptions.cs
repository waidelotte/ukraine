namespace Ukraine.Presentation.GraphQl.Options;

public class UkraineGraphQlWebOptions
{
	public string Path { get; set; } = Constants.DEFAULT_PATH;

	public string? VoyagerPath { get; set; }

	public bool UseBananaCakePopTool { get; set; }
}