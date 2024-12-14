using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

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
			try
			{
				var e = await _dbContext.GoodEntity.AddAsync(good);
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
			var good = await _dbContext.GoodEntity
				.AsNoTracking()
				.Include(x => x.Category)
				.Include(x => x.Images)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (good == null)
				return Result.Failure<Good>("Not found");
			return Result.Success(good);
		}

		public async Task<Result<List<Good>>> GetByCategoryIdWithPagination(int categoryId, int page, int pageSize)
		{
			var goods = await _dbContext.GoodEntity
				.AsNoTracking()
				.Where(x => x.CategoryId == categoryId)
				.Include(x => x.Images)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			return Result.Success(goods);
		}

		public async Task<Result<List<Good>>> GetByCategoryId(int categoryId)
		{
			var goods = await _dbContext.GoodEntity
				.AsNoTracking()
				.Where(x => x.CategoryId == categoryId)
				.Include(x => x.Images)
				.ToListAsync();
			return Result.Success(goods);
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
					.SetProperty(x => x.Count, x => newGood.Count)
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
	}
}
