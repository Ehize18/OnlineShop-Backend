using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.Goods;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/goods")]
	public class GoodController : ControllerBase
	{
		private readonly IGoodsService _goodsService;
		private readonly IImagesService _imagesService;

		public GoodController(IGoodsService goodsService, IWebHostEnvironment webHostEnvironment, IImagesService imagesService, IServer server)
		{
			_goodsService = goodsService;
			_imagesService = imagesService;
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> Add(GoodRequest request)
		{
			var result = await _goodsService.AddGood(request.name, request.description, request.price, request.categoryId);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		[HttpPost("{id:int}/images")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> AddImage(int id, IFormFile image)
		{
			if (!Directory.Exists("images"))
				Directory.CreateDirectory("images");
			var path = "images/" + image.FileName;
			var task = _imagesService.AddImage(image.FileName, path, id);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}
			var result = await task;
            if (result.IsFailure)
				return BadRequest(result.Error);
            return Ok();
		}

		[HttpPatch("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> UpdateGood(int id, PatchGood patch)
		{
			var goodResult = await _goodsService.GetGoodById(id);
			if (goodResult.IsFailure)
				return BadRequest(goodResult.Error);
			var good = goodResult.Value;
			var newGood = new Good(
				good.Id,
				patch.IsFieldPresent(nameof(good.Name)) ? patch.Name : good.Name,
				patch.IsFieldPresent(nameof(good.Description)) ? patch.Description : good.Description,
				patch.IsFieldPresent(nameof(good.Price)) ? (int)patch.Price : good.Price,
				patch.IsFieldPresent(nameof(good.CategoryId)) ? (int)patch.CategoryId : good.CategoryId,
				good.CreatedAt, good.UpdatedAt
				);
			var pathResult = await _goodsService.UpdateGood(id, newGood);
			if (pathResult.IsFailure)
				return BadRequest(pathResult.Error);
			return Ok();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> DeleteGood(int id)
		{
			var result = await _goodsService.DeleteGood(id);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}

		[HttpGet("{id:int}/images")]
		public async Task<ActionResult> GetImagesId(int id)
		{
			var imageResult = await _imagesService.GetImagesByGoodId(id);
			if (imageResult.IsFailure)
				return BadRequest(imageResult.Error);
			var images = imageResult.Value;
			var result = new List<int>();
			foreach (var image in images)
			{
				result.Add((int)image.Id);
			}
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> GetBuId(int id)
		{
			var goodResult = await _goodsService.GetGoodById(id);
			if (goodResult.IsFailure)
				return BadRequest(goodResult.Error);
			var response = new GoodResponse(
				(int)goodResult.Value.Id,
				goodResult.Value.Name,
				goodResult.Value.Description,
				goodResult.Value.Price,
				goodResult.Value.CategoryId,
				goodResult.Value.Images.Select(x => x.Id).ToList(),
				goodResult.Value.CreatedAt,
				goodResult.Value.UpdatedAt
				);
			return Ok(response);
		}
	}
}
