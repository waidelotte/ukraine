using AutoMapper;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.Mappings;

public class ExampleEntityMappingProfile : Profile
{
	public ExampleEntityMappingProfile()
	{
		CreateMap<ExampleEntity, ExampleEntityDTO>();
	}
}