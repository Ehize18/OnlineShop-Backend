using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.GoodCategories;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/good-categories")]
	public class GoodCategoriesController : ControllerBase
	{
		private readonly IGoodCategoriesService _categoriesService;

		public GoodCategoriesController(IGoodCategoriesService categoriesService)
		{
			_categoriesService = categoriesService;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> GetCategoryById(int id)
		{
			var result = await _categoriesService.GetCategoryById(id);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok(result.Value);
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
	}
}
