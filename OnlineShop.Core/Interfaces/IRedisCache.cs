using CSharpFunctionalExtensions;

namespace OnlineShop.Core.Interfaces
{
	public interface IRedisCache
	{
		Task<Result<int>> GetVerificationCode(string email);
		Task SaveVerificationCode(string email, int verificationCode);
	}
}