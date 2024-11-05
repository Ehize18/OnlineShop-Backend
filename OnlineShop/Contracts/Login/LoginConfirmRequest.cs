using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Contracts.Login
{
	public record LoginConfirmRequest(
		[Required][EmailAddress] string email, int otp);
}
