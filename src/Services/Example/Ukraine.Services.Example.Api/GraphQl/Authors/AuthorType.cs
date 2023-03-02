using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

internal sealed class AuthorType : ObjectType<AuthorDTO>
{
	protected override void Configure(IObjectTypeDescriptor<AuthorDTO> descriptor)
	{
		descriptor.Name("Author");
		descriptor.Field(f => f.Id).ID().IsProjected();
		descriptor.Field(f => f.Status).Authorize(Constants.Policy.GRAPHQL_ADMIN);
	}
}