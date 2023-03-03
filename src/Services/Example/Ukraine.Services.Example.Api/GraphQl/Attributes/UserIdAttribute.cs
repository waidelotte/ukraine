namespace Ukraine.Services.Example.Api.GraphQl.Attributes;

public class UserIdAttribute : GlobalStateAttribute
{
	public UserIdAttribute() : base("UserId") { }
}