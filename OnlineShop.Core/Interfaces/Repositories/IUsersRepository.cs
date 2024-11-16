using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IUsersRepository
	{
		Task<Result> AddUser(User user);
		Task<Result<User>> GetUserByEmail(string email);
		Task<Result<User>> GetUserByRefreshToken(string token);
		Task<Result> UpdateRefreshToken(int id, string refreshToken);
	}
}