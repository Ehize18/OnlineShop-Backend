using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/image")]
	public class ImageController : ControllerBase
	{
		private readonly IImagesService _imagesService;

		public ImageController(IImagesService imagesService)
		{
			_imagesService = imagesService;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> GetImage(int id)
		{
			var imageResult = await _imagesService.GetImageById(id);
			if (imageResult.IsFailure)
				return BadRequest(imageResult.Error);
			var image = imageResult.Value;
			var result = new List<int>();
			var path = image.Path;
			var stream = new FileStream(path, FileMode.Open);
			var fileType = "image/png";
			return File(stream, fileType, image.Name);
		}

		[HttpDelete("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> DeleteImage(int id)
		{
			var result = await _imagesService.DeleteImage(id);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}
	}
}
