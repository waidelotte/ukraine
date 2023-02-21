using System.ComponentModel.DataAnnotations;

namespace Ukraine.EventBus.Options;

public class UkraineDaprOptions
{
	[Required]
	public required string PubSubName { get; set; }
}