using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Entities;

public class ExampleEntity : AggregateRoot<Guid>
{
	public string? StringValue { get; private set;}
	public int? IntValue { get; private set; }
	
	public ExampleEntity(string? stringValue, int? intValue)
	{
		StringValue = stringValue;
		IntValue = intValue;
	}
}