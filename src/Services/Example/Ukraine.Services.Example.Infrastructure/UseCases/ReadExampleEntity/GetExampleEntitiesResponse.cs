using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public record GetExampleEntitiesResponse(IEnumerable<ExampleEntityDTO> Values, int Total);