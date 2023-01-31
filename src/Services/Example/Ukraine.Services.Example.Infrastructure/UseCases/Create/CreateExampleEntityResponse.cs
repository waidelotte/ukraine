using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Create;

public class CreateExampleEntityResponse
{
	public ExampleEntityDTO ExampleEntity { get; }
	
	public CreateExampleEntityResponse(ExampleEntityDTO entityDTO)
	{
		ExampleEntity = entityDTO;
	}
}