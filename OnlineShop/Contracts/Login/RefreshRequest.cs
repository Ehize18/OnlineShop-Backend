using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Contracts.Login
{
	public record RefreshRequest([Required] string oldToken);
}
