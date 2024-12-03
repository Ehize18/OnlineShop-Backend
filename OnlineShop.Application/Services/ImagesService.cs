using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.Application.Services
{
	public class ImagesService : IImagesService
	{
		private readonly IImageRepository _imageRepository;
		private readonly IGoodsRepository _goodsRepository;

		public ImagesService(IImageRepository imageRepository, IGoodsRepository goodsRepository)
		{
			_imageRepository = imageRepository;
			_goodsRepository = goodsRepository;
		}

		public async Task<Result> AddImage(string name, string path, int goodId)
		{
			var good = await _goodsRepository.GetById(goodId);
			return await _imageRepository.Add(new Image(null, name, path, good.Value, DateTime.UtcNow, DateTime.UtcNow));
		}

		public async Task<Result<Image>> GetImageById(int id)
		{
			return await _imageRepository.GetById(id);
		}

		public async Task<Result<List<Image>>> GetImagesByGoodId(int id)
		{
			return await _imageRepository.GetByGoodId(id);
		}

		public async Task<Result> DeleteImage(int id)
		{
			var imageResult = await _imageRepository.GetById(id);
			
			if (imageResult.IsFailure)
				return Result.Failure(imageResult.Error);
			try
			{
				File.Delete(imageResult.Value.Path);
			}
			catch (Exception ex)
			{
				return Result.Failure($"Failed to delete image {ex.Message}");
			}
			var task = _imageRepository.Delete(id);
			task.Wait();
			return task.Result;
		}
	}
}
