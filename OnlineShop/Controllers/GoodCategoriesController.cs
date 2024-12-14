using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.GoodCategories;
using OnlineShop.Contracts.Goods;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/good-categories")]
	public class GoodCategoriesController : ControllerBase
	{
		private readonly IGoodCategoriesService _categoriesService;
		private readonly IGoodsService _goodsService;

		public GoodCategoriesController(IGoodCategoriesService categoriesService, IGoodsService goodsService)
		{
			_categoriesService = categoriesService;
			_goodsService = goodsService;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> GetCategoryById(int id)
		{
			var result = await _categoriesService.GetCategoryById(id);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		[HttpGet("{id:int}/childs")]
		public async Task<ActionResult<List<CategoryWhitoutChilds>>> GetCategoryChilds(int id)
		{
			var categoriesResult = await _categoriesService.GetCategoriesByParentId(id);
			if (categoriesResult.IsFailure)
				return BadRequest(categoriesResult.Error);
			var categories = categoriesResult.Value;
			var result = new List<CategoryWhitoutChilds>();
			foreach (var category in categories)
			{
				result.Add(new CategoryWhitoutChilds((int)category.Id, category.Title, category.Description, (int)category.ParentId, category.CreatedAt, category.UpdatedAt));
			}
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult<AllCategoriesResponse>> GetAllCategories()
		{
			var categoriesResult = await _categoriesService.GetAllCategories();
			if (categoriesResult.IsFailure)
				return BadRequest(categoriesResult.Error);
			var categories = categoriesResult.Value;
			var count = GetTreeCount(categories);
			var response = new AllCategoriesResponse(count, categories);
			return Ok(response);
		}

		[HttpGet("{id:int}/goods")]
		public async Task<ActionResult<GoodCategoryGoodsResponse>> GetGoodsWithPagination(int id, [FromQuery]GoodCategoryGoodsRequest request)
		{
			var goodsResult = await _goodsService.GetGoodsByGategoryId(id, request.page, request.pageSize);
			if (goodsResult.IsFailure)
				return BadRequest(goodsResult.Error);
			var goods = goodsResult.Value;
			var goodsResponse = new List<GoodResponse>();
			foreach (var good in goods)
				goodsResponse.Add(
					new GoodResponse(
						(int)good.Id,
						good.Name,
						good.Description,
						good.Price,
						good.CategoryId,
						good.Images.Select(x => x.Id).ToList(),
						good.Count,
						good.CreatedAt,
						good.UpdatedAt
				));
			return Ok(new GoodCategoryGoodsResponse(goodsResponse.Count, goodsResponse));
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> AddCategory(GoodCategoryRequest request)
		{
			var result =  await _categoriesService.AddCategory(request.title, request.description, request.parentId);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}

		[HttpPatch("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> UpdateCategory(int id, PatchCategory patchCategory)
		{
			var categoryResult = await _categoriesService.GetCategoryById(id);
			if (categoryResult.IsFailure)
				return BadRequest(categoryResult.Error);
			var category = categoryResult.Value;
			var newCategory = new GoodCategory(
				category.Id,
				patchCategory.IsFieldPresent(nameof(category.Title)) ? patchCategory.Title : category.Title,
				patchCategory.IsFieldPresent(nameof(category.Description)) ? patchCategory.Description : category.Description,
				patchCategory.IsFieldPresent(nameof(category.ParentId)) ? patchCategory.ParentId : category.ParentId,
				category.CreatedAt,
				category.UpdatedAt
				);
			var result = await _categoriesService.UpdateCategory(id, newCategory);
			if (result.IsFailure) return
					BadRequest(result.Error);
			return Ok();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> DeleteCategory(int id, bool cascade, int? newParentId)
		{
			var deleteResult = new Result();
			if (cascade)
				deleteResult = await _categoriesService.DeleteCategoryCascade(id);
			else
				deleteResult = await _categoriesService.DeleteCategoryWithSaveChilds(id, newParentId);
			if (deleteResult.IsFailure)
				return BadRequest(deleteResult.Error);
			return Ok();
		}
		private int GetTreeCount(List<GoodCategory> goodCategories)
		{
			int count = goodCategories.Count;
			foreach (var goodCategory in goodCategories)
			{
				if (goodCategory.Childs.Count > 0)
					count += GetTreeCount(goodCategory.Childs);
			}
			return count;
		}
	}
}
