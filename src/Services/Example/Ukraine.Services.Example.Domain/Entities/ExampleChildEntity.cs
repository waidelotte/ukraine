using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Entities;

public class ExampleChildEntity : AggregateRoot<Guid>
{
	public int NotNullIntValue { get; private set; }

	public Guid ExampleEntityId { get; private set; }
	public ExampleEntity ExampleEntity { get; private set; } = null!;
	
	public ExampleChildEntity(int notNullIntValue)
	{
		NotNullIntValue = notNullIntValue;
	}
}