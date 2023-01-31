using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public class GetExampleEntitiesResponse
{
	public IEnumerable<ExampleEntityDTO> Values { get; set; } = Array.Empty<ExampleEntityDTO>();
	public int Total { get; set; }
}