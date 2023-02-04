using Ukraine.Domain.Models;

namespace Ukraine.Services.Example.Domain.Models;

public sealed class ExampleEntity : AggregateRoot<Guid>
{
	public string? StringValue { get; set; }
	public int? IntValue { get; set; }

	public ICollection<ExampleChildEntity> ChildEntities { get; set; } = new List<ExampleChildEntity>();
}