using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public class ExampleEntity : AggregateRoot<Guid>
{
	public string? StringValue { get; private set;}
	public int? IntValue { get; private set; }

	public ICollection<ExampleChildEntity> ChildEntities { get; } = new List<ExampleChildEntity>();
	
	public ExampleEntity(string? stringValue, int? intValue)
	{
		StringValue = stringValue;
		IntValue = intValue;
	}
}