﻿using AutoMapper;
using Ukraine.Services.Example.Domain.Entities;
using Ukraine.Services.Example.Infrastructure.DTO;

namespace Ukraine.Services.Example.Infrastructure.Mappings;

public class ExampleChildEntityMappingProfile : Profile
{
	public ExampleChildEntityMappingProfile()
	{
		CreateMap<ExampleChildEntity, ExampleChildEntityDTO>();
	}
}