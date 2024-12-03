using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IImageRepository
	{
		Task<Result> Add(Image image);
		Task<Result<List<Image>>> GetByGoodId(int id);
		Task<Result<Image>> GetById(int id);
		Task<Result> Delete(int id);
	}
}