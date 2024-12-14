using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.Application.Services
{
	public class GoodsService : IGoodsService
	{
		private readonly IGoodsRepository _goodsRepository;
		private readonly IGoodCategoriesRepository _goodCategoriesRepository;

		public GoodsService(IGoodsRepository goodsRepository, IGoodCategoriesRepository goodCategoriesRepository)
		{
			_goodsRepository = goodsRepository;
			_goodCategoriesRepository = goodCategoriesRepository;
		}

		public async Task<Result<int>> AddGood(string name, string description, int price, int categoryId, int count)
		{
			var categoryResult = await _goodCategoriesRepository.GetCategoryById(categoryId);
			if (categoryResult.IsFailure)
				return Result.Failure<int>(categoryResult.Error);
			var good = new Good(null, name, description, price, categoryResult.Value, count);
			var result = await _goodsRepository.Add(good);
			return result;
		}

		public async Task<Result<Good>> GetGoodById(int id)
		{
			var goodResult = await _goodsRepository.GetById(id);
			return goodResult;
		}

		public async Task<Result<List<Good>>> GetGoodsByGategoryId(int categoryId, int? page = null, int? pageSize = null)
		{
			if (page == null || pageSize == null)
				return await _goodsRepository.GetByCategoryId(categoryId);
			return await _goodsRepository.GetByCategoryIdWithPagination(categoryId, (int)page, (int)pageSize);
		}

		public async Task<Result> UpdateGood(int id, Good good)
		{
			return await _goodsRepository.Update(id, good);
		}

		public async Task<Result> DeleteGood(int id)
		{
			var goodResult = await _goodsRepository.GetById(id);
			if (goodResult.IsFailure)
				return Result.Failure(goodResult.Error);
			var result = await _goodsRepository.Delete(id);
			return result;
		}
	}
}
