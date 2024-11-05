using CSharpFunctionalExtensions;

namespace OnlineShop.Core.Interfaces
{
	public interface ILoginService
	{
		Task LoginRequest(string email);
		Task<Result<List<string>>> LoginConfirm(string email, int otp);
		Task<Result<string>> RefreshToken(string refreshToken, string oldToken);
	}
}