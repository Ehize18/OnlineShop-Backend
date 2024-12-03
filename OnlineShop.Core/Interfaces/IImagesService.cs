using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IImagesService
	{
		Task<Result> AddImage(string name, string path, int goodId);
		Task<Result<Image>> GetImageById(int id);
		Task<Result<List<Image>>> GetImagesByGoodId(int id);
		Task<Result> DeleteImage(int id);
	}
}