using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class DeliveryMethodRepository : IDeliveryMethodRepository
	{
		public readonly OnlineStoreDbContext _dbContext;

		public DeliveryMethodRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<int>> Add(DeliveryMethod deliveryMethod)
		{
			try
			{
				var e = await _dbContext.DeliveryMethods
					.AddAsync(deliveryMethod);
				await _dbContext.SaveChangesAsync();
				return Result.Success((int)e.Entity.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<int>(ex.Message);
			}
		}

		public async Task<Result<DeliveryMethod>> GetById(int id)
		{
			var method = await _dbContext.DeliveryMethods
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			if (method == null)
				return Result.Failure<DeliveryMethod>("Not found");
			return Result.Success(method);
		}

		public async Task<Result<List<DeliveryMethod>>> GetAll()
		{
			return await _dbContext.DeliveryMethods
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Result> Update(DeliveryMethod deliveryMethod)
		{
			try
			{
				await _dbContext.DeliveryMethods
					.Where(x => x.Id == deliveryMethod.Id)
					.ExecuteUpdateAsync(s => s
					.SetProperty(x => x.Title, deliveryMethod.Title)
					.SetProperty(x => x.Description, deliveryMethod.Description)
					.SetProperty(x => x.UpdatedAt, DateTime.UtcNow));
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> Delete(int deliveryMethodId)
		{
			try
			{
				await _dbContext.DeliveryMethods
					.Where(x => x.Id == deliveryMethodId)
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
