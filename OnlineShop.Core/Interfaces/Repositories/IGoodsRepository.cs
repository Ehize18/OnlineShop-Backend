using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IGoodsRepository
	{
		Task<Result<int>> Add(Good good);
		Task<Result<Good>> GetById(int id);
		Task<Result<List<Good>>> GetByCategoryIdWithPagination(int categoryId, int page, int pageSize);
		Task<Result<List<Good>>> GetByCategoryId(int categoryId);
		Task<Result> Update(int id, Good newGood);
		Task<Result> Delete(int id);
	}
}