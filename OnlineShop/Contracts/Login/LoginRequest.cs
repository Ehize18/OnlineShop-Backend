using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Contracts.Login
{
	public record LoginRequest(
		[Required][EmailAddress] string email);
}
