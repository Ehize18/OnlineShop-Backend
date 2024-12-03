using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;
using OnlineShop.DataBase.PostgreSQL.Entities;

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
			var entity = MapToEntity(image);
			try
			{
				await _dbContext.AddAsync(entity);
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
			var entity = await _dbContext.Images
				.AsNoTracking()
				.Include(x => x.Good)
				.ThenInclude(x => x.Category)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
				return Result.Failure<Image>("Not Found");
			var image = MapEntity(entity);
			return Result.Success(image);
		}

		public async Task<Result<List<Image>>> GetByGoodId(int id)
		{
			var entities = await _dbContext.Images
				.AsNoTracking()
				.Where(x => x.GoodId == id)
				.Include(x => x.Good)
				.ThenInclude(x => x.Category)
				.ToListAsync();
			if (entities.Count == 0)
				return Result.Failure<List<Image>>("Not Found");
			var result = new List<Image>();
			foreach (var entity in entities)
			{
				result.Add(MapEntity(entity));
			}
			return Result.Success(result);
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

		private ImageEntity MapToEntity(Image image)
		{
			return new ImageEntity
			{
				Id = image.Id,
				Name = image.Name,
				GoodId = image.GoodId,
				Path = image.Path
			};
		}

		private Image MapEntity(ImageEntity entity)
		{
			return new Image(entity.Id, entity.Name, entity.Path, Mapper.MapEntity(entity.Good), entity.CreatedAt, entity.UpdatedAt);
		}
	}
}
