using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

public sealed class ExampleSpec : Specification<ExampleEntity>
{
	private ExampleSpec() { }
	
	private ExampleSpec(Guid id)
	{
		Query.Where(w => w.Id == id);
	}
	
	public static ExampleSpec Create()
	{
		return new ExampleSpec();
	}
	
	public static ExampleSpec Create(Guid id)
	{
		return new ExampleSpec(id);
	}
}
