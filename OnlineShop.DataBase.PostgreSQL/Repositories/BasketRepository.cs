using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly OnlineStoreDbContext _dbContext;

		public BasketRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<Basket>> Add(Basket basket)
		{
			try
			{
				var e = await _dbContext.AddAsync(basket);
				await _dbContext.SaveChangesAsync();
				return Result.Success(e.Entity);
			}
			catch (Exception ex)
			{
				return Result.Failure<Basket>(ex.Message);
			}
		}

		public async Task<Result<List<Basket>>> GetAllByUserId(int userId)
		{
			var baskets = await _dbContext.Baskets
				.Where(x => x.UsertId == userId)
				.ToListAsync();
			if (baskets.Count == 0)
				return Result.Failure<List<Basket>>("Baskets Not Found");
			return Result.Success(baskets);
		}

		public async Task<Result<Basket>> GetCurrent(int userId)
		{
			var basket = await _dbContext.Baskets
				.Include(x => x.Items)
				.ThenInclude(x => x.Good)
				.FirstOrDefaultAsync(x => (x.UsertId == userId) && (!x.isCompleted));
			if (basket == null)
				return Result.Failure<Basket>("No Basket");
			return Result.Success(basket);
		}
	}
}
