using Ukraine.Infrastructure.Mediator.Requests;

namespace Ukraine.Services.Example.Infrastructure.UseCases.ReadExampleEntity;

public record GetExampleEntitiesRequest : BasePagedRequest<GetExampleEntitiesResponse>;