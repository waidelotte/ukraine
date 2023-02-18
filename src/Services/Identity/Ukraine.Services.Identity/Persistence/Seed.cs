using Microsoft.AspNetCore.Identity;
using Ukraine.Services.Identity.Exceptions;
using Ukraine.Services.Identity.Models;

namespace Ukraine.Services.Identity.Persistence;

public static class Seed
{
	public static async Task SeedDevAsync(IServiceScope scope)
	{
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UkraineUser>>();

		var devUser = await userManager.FindByNameAsync("dev");

		if (devUser == null)
		{
			devUser = new UkraineUser
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "dev",
				Email = "dev@email.com",
				EmailConfirmed = true,
				PhoneNumber = "1234567890",
				PhoneNumberConfirmed = true
			};

			var result = await userManager.CreateAsync(devUser, "devdev");

			if (!result.Succeeded)
			{
				throw IdentityException.Exception(result.Errors.First().Description);
			}
		}
	}
}