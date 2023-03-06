using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ukraine.Services.Identity.ViewModels.Grants;

namespace Ukraine.Services.Identity.Controllers;

[Authorize]
public class GrantsController : Controller
{
	private readonly IIdentityServerInteractionService _interaction;
	private readonly IClientStore _clients;
	private readonly IResourceStore _resources;

	public GrantsController(
		IIdentityServerInteractionService interaction,
		IClientStore clients,
		IResourceStore resources)
	{
		_interaction = interaction;
		_clients = clients;
		_resources = resources;
	}

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var grants = await _interaction.GetAllUserGrantsAsync();

		var list = new List<GrantsItemViewModel>();
		foreach (var grant in grants)
		{
			var client = await _clients.FindClientByIdAsync(grant.ClientId);
			if (client != null)
			{
				var resources = await _resources.FindResourcesByScopeAsync(grant.Scopes);

				var item = new GrantsItemViewModel
				{
					ClientId = client.ClientId,
					ClientName = client.ClientName ?? client.ClientId,
					ClientUrl = client.ClientUri,
					Description = grant.Description,
					Created = grant.CreationTime,
					Expires = grant.Expiration,
					IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
					ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
				};

				list.Add(item);
			}
		}

		return View("Index", new GrantsViewModel(list));
	}
}