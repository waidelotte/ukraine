using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.UseCases.GetAuthorById;

namespace Ukraine.Services.Example.Api.GraphQl.Authors.GetAuthorById;

public class GetAuthorByIdPayloadType : ObjectType<GetAuthorByIdResponse>
{
	protected override void Configure(IObjectTypeDescriptor<GetAuthorByIdResponse> descriptor)
	{
		descriptor.Name("GetAuthorByIdPayload");
	}
}