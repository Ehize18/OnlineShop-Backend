using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IBasketItemRepository
	{
		Task<Result<int>> Add(BasketItem item);
		Task<Result<List<BasketItem>>> GetByBasketId(int basketId);
		Task<Result> UpdateItem(int id, int count);
		Task<Result> DeleteItem(int id);
	}
}