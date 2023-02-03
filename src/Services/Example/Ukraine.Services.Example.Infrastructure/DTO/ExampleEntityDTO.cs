namespace Ukraine.Services.Example.Infrastructure.DTO;

public record ExampleEntityDTO(Guid Id, string? StringValue, int? IntValue, IEnumerable<ExampleChildEntityDTO> ChildEntities);