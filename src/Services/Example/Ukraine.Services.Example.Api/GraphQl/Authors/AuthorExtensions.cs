using HotChocolate;
using HotChocolate.Types;
using Ukraine.Services.Example.Infrastructure.DTOs;

namespace Ukraine.Services.Example.Api.GraphQl.Authors;

[ExtendObjectType(typeof(AuthorDTO))]
public class AuthorExtensions
{
	public string FullInformation([Parent] AuthorDTO author)
	{
		return $"Full Name: {author.FullName}. Age: {(author.Age.HasValue ? author.Age.Value : "unknown")}";
	}
}