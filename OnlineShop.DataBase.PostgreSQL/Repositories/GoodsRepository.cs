using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;
using OnlineShop.DataBase.PostgreSQL.Entities;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class GoodsRepository : IGoodsRepository
	{
		private readonly OnlineStoreDbContext _dbContext;

		public GoodsRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<int>> Add(Good good)
		{
			var entity = MapToEntity(good);
			try
			{
				var e = await _dbContext.AddAsync(entity);
				await _dbContext.SaveChangesAsync();
				return Result.Success((int)e.Entity.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<int>(ex.Message);
			}
		}

		public async Task<Result<Good>> GetById(int id)
		{
			var entity = await _dbContext.GoodEntity
				.AsNoTracking()
				.Include(x => x.Category)
				.Include(x => x.Images)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null)
				return Result.Failure<Good>("Not found");
			var good = MapEntity(entity);
			return Result.Success(good);
		}

		public async Task<Result<List<Good>>> GetByCategoryIdWithPagination(int categoryId, int page, int pageSize)
		{
			var entities = await _dbContext.GoodEntity
				.AsNoTracking()
				.Where(x => x.CategoryId == categoryId)
				.Include(x => x.Images)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			var result = new List<Good>();
			foreach (var entity in entities)
				result.Add(MapEntity(entity));
			return Result.Success(result);
		}

		public async Task<Result<List<Good>>> GetByCategoryId(int categoryId)
		{
			var entities = await _dbContext.GoodEntity
				.AsNoTracking()
				.Where(x => x.CategoryId == categoryId)
				.Include(x => x.Images)
				.ToListAsync();
			var result = new List<Good>();
			foreach (var entity in entities)
				result.Add(MapEntity(entity));
			return Result.Success(result);
		}

		public async Task<Result> Update(int id, Good newGood)
		{
			try
			{
				await _dbContext.GoodEntity
					.Where(x => x.Id == id)
					.ExecuteUpdateAsync(s => s
					.SetProperty(x => x.Name, x => newGood.Name)
					.SetProperty(x => x.Description, x => newGood.Description)
					.SetProperty(x => x.Price, x => newGood.Price)
					.SetProperty(x => x.CategoryId, x => newGood.CategoryId)
					.SetProperty(x => x.UpdatedAt, x => DateTime.UtcNow));
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> Delete(int id)
		{
			try
			{
				await _dbContext.GoodEntity
					.Where(x => x.Id == id)
					.ExecuteDeleteAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		private GoodEntity MapToEntity(Good good)
		{
			return new GoodEntity
			{
				Id = good.Id,
				Name = good.Name,
				Description = good.Description,
				CategoryId = good.CategoryId,
				CreatedAt = good.CreatedAt,
				UpdatedAt = good.UpdatedAt
			};
		}

		private Good MapEntity(GoodEntity goodEntity)
		{
			var category = goodEntity.Category == null ? null : Mapper.MapEntity(goodEntity.Category);
			var images = new List<Image>();
			foreach (var image in goodEntity.Images)
				images.Add(Mapper.MapEntity(image));
			return new Good(goodEntity.Id, goodEntity.Name, goodEntity.Description, goodEntity.Price, goodEntity.CategoryId,
				category, images, goodEntity.CreatedAt, goodEntity.UpdatedAt);
		}
	}
}
