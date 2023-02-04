using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class ExampleChildEntity : AggregateRoot<Guid>
{
	public int NotNullIntValue { get; set; }

	public Guid ExampleEntityId { get; set; }
	public ExampleEntity ExampleEntity { get; set; } = null!;
}