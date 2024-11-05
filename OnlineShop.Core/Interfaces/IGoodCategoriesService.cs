using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IGoodCategoriesService
	{
		Task<Result> AddCategory(string title, string description, int? parentId);
		Task<Result<List<GoodCategory>>> GetCategoriesByParentId(int id);
		Task<Result<GoodCategory>> GetCategoryById(int id);
	}
}