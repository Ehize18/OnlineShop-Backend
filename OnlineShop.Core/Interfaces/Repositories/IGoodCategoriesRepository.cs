using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IGoodCategoriesRepository
	{
		Task<Result> AddCategory(GoodCategory category);
		Task<Result> Delete(int id);
		Task<Result<List<GoodCategory>>> GetAllCategories();
		Task<Result<List<GoodCategory>>> GetCategoriesByParrentId(int? id);
		Task<Result<GoodCategory>> GetCategoryById(int id);
		Task<Result> Update(int id, GoodCategory category);
	}
}