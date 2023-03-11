using System.ComponentModel.DataAnnotations;

namespace Ukraine.Gateway.GraphQl.Options;

public class GraphQlSchemaOptions
{
	[Required]
	public required string Name { get; set; }

	[Required]
	public required Uri Url { get; set; }
}