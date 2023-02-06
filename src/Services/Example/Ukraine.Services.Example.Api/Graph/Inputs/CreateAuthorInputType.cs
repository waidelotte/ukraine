﻿using Ukraine.Services.Example.Infrastructure.UseCases.CreateAuthor;

namespace Ukraine.Services.Example.Api.Graph.Inputs;

public class CreateAuthorInputType : InputObjectType<CreateAuthorRequest>
{
	protected override void Configure(IInputObjectTypeDescriptor<CreateAuthorRequest> descriptor)
	{
		descriptor.Name("CreateAuthorInput");
	}
}