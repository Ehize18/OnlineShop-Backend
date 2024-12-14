using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IBasketRepository
	{
		Task<Result<Basket>> Add(Basket basket);
		Task<Result<List<Basket>>> GetAllByUserId(int userId);
		Task<Result<Basket>> GetCurrent(int userId);
	}
}