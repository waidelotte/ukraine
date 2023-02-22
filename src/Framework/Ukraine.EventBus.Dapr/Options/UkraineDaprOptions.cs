using System.ComponentModel.DataAnnotations;

namespace Ukraine.EventBus.Dapr.Options;

public class UkraineDaprOptions
{
	[Required]
	public required string PubSubName { get; set; }
}