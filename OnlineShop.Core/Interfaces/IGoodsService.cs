using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IGoodsService
	{
		Task<Result<int>> AddGood(string name, string description, int price, int categoryId);
		Task<Result<Good>> GetGoodById(int id);
		Task<Result<List<Good>>> GetGoodsByGategoryId(int categoryId, int? page, int? pageSize);
		Task<Result> UpdateGood(int id, Good good);
		Task<Result> DeleteGood(int id);
	}
}