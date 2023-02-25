﻿using AutoMapper;
using Ukraine.Services.Example.Domain.Models;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Infrastructure.Mapper;

public class AuthorProfile : Profile
{
	public AuthorProfile()
	{
		CreateMap<Author, AuthorDTO>();
	}
}