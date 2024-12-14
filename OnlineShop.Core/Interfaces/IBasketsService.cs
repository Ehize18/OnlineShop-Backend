using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IBasketsService
	{
		Task<Result> AddItemToBasket(int goodId, string userEmail);
		Task<Result<Basket>> GetCurrentBasket(string userEmail);
		Task<Result> ChangeItemCountInBasket(string userEmail, int basketItemId, int count);
		Task<Result> DeleteItemFromBasket(string userEmail, int basketItemId);
	}
}