using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly OnlineStoreDbContext _dbContext;

		public ImageRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result> Add(Image image)
		{
			try
			{
				await _dbContext.AddAsync(image);
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result<Image>> GetById(int id)
		{
			var image = await _dbContext.Images
				.AsNoTracking()
				.Include(x => x.Good)
				.ThenInclude(x => x.Category)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (image == null)
				return Result.Failure<Image>("Not Found");
			return Result.Success(image);
		}

		public async Task<Result<List<Image>>> GetByGoodId(int id)
		{
			var images = await _dbContext.Images
				.AsNoTracking()
				.Where(x => x.GoodId == id)
				.Include(x => x.Good)
				.ThenInclude(x => x.Category)
				.ToListAsync();
			if (images.Count == 0)
				return Result.Failure<List<Image>>("Not Found");
			return Result.Success(images);
		}

		public async Task<Result> Delete(int id)
		{
			try
			{
				await _dbContext.Images
					.Where(x => x.Id == id)
					.ExecuteDeleteAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
	}
}
