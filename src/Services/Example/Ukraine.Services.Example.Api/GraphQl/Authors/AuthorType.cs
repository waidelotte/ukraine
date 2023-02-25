using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

public class AuthorType : ObjectType<AuthorDTO>
{
	protected override void Configure(IObjectTypeDescriptor<AuthorDTO> descriptor)
	{
		descriptor.Name("Author");
		descriptor.Field(f => f.Id).ID();
	}
}