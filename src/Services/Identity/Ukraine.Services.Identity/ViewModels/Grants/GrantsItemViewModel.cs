namespace Ukraine.Services.Identity.ViewModels.Grants;

public class GrantsItemViewModel
{
	public string? ClientId { get; init; }

	public string? ClientName { get; init; }

	public string? ClientUrl { get; init; }

	public string? Description { get; init; }

	public DateTime Created { get; init; }

	public DateTime? Expires { get; init; }

	public IReadOnlyCollection<string> IdentityGrantNames { get; init; } = Array.Empty<string>();

	public IReadOnlyCollection<string> ApiGrantNames { get; init; } = Array.Empty<string>();
}