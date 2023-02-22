using System.ComponentModel.DataAnnotations;

namespace Ukraine.Dapr.Options;

public class UkraineDaprOptions
{
	[Required]
	public required string PubSubName { get; set; }
}