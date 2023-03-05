using System.ComponentModel.DataAnnotations;

namespace Ukraine.Services.Identity.Token.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required]
		public string? UserName { get; set; }

		[Required]
		[EmailAddress]
		public string? Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password")]
		public string? ConfirmPassword { get; set; }
	}
}