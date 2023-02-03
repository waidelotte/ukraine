using Ardalis.Specification;
using Ukraine.Services.Example.Domain.Entities;

namespace Ukraine.Services.Example.Infrastructure.EfCore.Specifications;

public sealed class ExampleSpec : Specification<ExampleEntity>
{
	private ExampleSpec() { }
	
	private ExampleSpec(Guid id)
	{
		Query.Where(w => w.Id == id);
	}
	
	private ExampleSpec(int pageIndex, int pageSize)
	{
		Query
			.Skip((pageIndex - 1) * pageSize)
			.Take(pageSize);
	}
	
	public static ExampleSpec Create()
	{
		return new ExampleSpec();
	}
	
	public static ExampleSpec Create(Guid id)
	{
		return new ExampleSpec(id);
	}

	public static ExampleSpec Create(int pageIndex, int pageSize)
	{
		return new ExampleSpec(pageIndex, pageSize);
	}
}
