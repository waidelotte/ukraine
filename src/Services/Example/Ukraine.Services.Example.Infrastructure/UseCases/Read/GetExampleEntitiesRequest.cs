using Ukraine.Infrastructure.Mediator.Requests;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public record GetExampleEntitiesRequest : BasePagedRequest<GetExampleEntitiesResponse>;