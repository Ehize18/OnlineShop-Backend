using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.Application.Services
{
	public class GoodCategoriesService : IGoodCategoriesService
	{
		private readonly IGoodCategoriesRepository _categoriesRepository;

		public GoodCategoriesService(IGoodCategoriesRepository categoriesRepository)
		{
			_categoriesRepository = categoriesRepository;
		}

		public async Task<Result> AddCategory(string title, string description, int? parentId)
		{
			var category = new GoodCategory(title, description, parentId);
			var result = await _categoriesRepository.AddCategory(category);
			return result;
		}

		public async Task<Result<GoodCategory>> GetCategoryById(int id)
		{
			var categoryResult = await _categoriesRepository.GetCategoryById(id);
			return categoryResult;
		}

		public async Task<Result<List<GoodCategory>>> GetCategoriesByParentId(int id)
		{
			var categoryResult = await _categoriesRepository.GetCategoriesByParrentId(id);
			return categoryResult;
		}
	}
}
