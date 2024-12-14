using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class BasketItemRepository : IBasketItemRepository
	{
		private readonly OnlineStoreDbContext _dbContext;

		public BasketItemRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<int>> Add(BasketItem item)
		{
			try
			{
				var e = await _dbContext.AddAsync(item);
				await _dbContext.SaveChangesAsync();
				return Result.Success((int)e.Entity.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<int>(ex.Message);
			}
		}

		public async Task<Result<List<BasketItem>>> GetByBasketId(int basketId)
		{
			var items = await _dbContext.BasketItems
				.Include(x => x.Good)
				.Where(x => x.BasketId == basketId)
				.ToListAsync();
			if (items.Count == 0)
				return Result.Failure<List<BasketItem>>("Items Not Found");
			return Result.Success(items);
		}

		public async Task<Result> UpdateItem(int id, int count)
		{
			try
			{
				await _dbContext.BasketItems
					.Where(x => x.Id == id)
					.ExecuteUpdateAsync(s => s
					.SetProperty(x => x.Count, count));
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
		
		public async Task<Result> DeleteItem(int id)
		{
			try
			{
				await _dbContext.BasketItems
					.Where(x => x.Id == id)
					.ExecuteDeleteAsync();
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
	}
}
