using MediatR;

namespace Ukraine.Services.Example.Infrastructure.UseCases.Read;

public record GetExampleEntitiesRequest(int PageIndex, int PageSize) : IRequest<GetExampleEntitiesResponse>;