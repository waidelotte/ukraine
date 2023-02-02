using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public record GetExampleEntitiesResponse(IEnumerable<ExampleEntityDTO> Values, int Total);