﻿using MediatR;
using Ukraine.Services.Example.Domain.Models;

namespace Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

public sealed record CreateAuthorRequest(string FullName, int? Age) : IRequest<CreateAuthorResponse>;