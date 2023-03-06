namespace Ukraine.Services.Identity.ViewModels.Grants;

public class GrantsViewModel
{
	public GrantsViewModel(IReadOnlyCollection<GrantsItemViewModel>? items)
	{
		Grants = items ?? Array.Empty<GrantsItemViewModel>();
	}

	public IReadOnlyCollection<GrantsItemViewModel> Grants { get; }
}