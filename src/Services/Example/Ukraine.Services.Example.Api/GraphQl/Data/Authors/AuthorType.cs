using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Data.Authors;

internal sealed class AuthorType : ObjectType<AuthorDTO>
{
	protected override void Configure(IObjectTypeDescriptor<AuthorDTO> descriptor)
	{
		descriptor.Name("Author");
		descriptor.Field(f => f.Id).ID().IsProjected();
		descriptor.Field(f => f.Email).Authorize(Constants.Policy.GRAPHQL_ADMIN);
	}
}