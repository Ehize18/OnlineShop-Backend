using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class PaymentMethodRepository : IPaymentMethodRepository
	{
		public readonly OnlineStoreDbContext _dbContext;

		public PaymentMethodRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<int>> Add(PaymentMethod paymentMethod)
		{
			try
			{
				var e = await _dbContext.PaymentMethods
					.AddAsync(paymentMethod);
				await _dbContext.SaveChangesAsync();
				return Result.Success((int)e.Entity.Id);
			}
			catch (Exception ex)
			{
				return Result.Failure<int>(ex.Message);
			}
		}

		public async Task<Result<List<PaymentMethod>>> GetAll()
		{
			return await _dbContext.PaymentMethods
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Result<PaymentMethod>> GetById(int id)
		{
			var method = await _dbContext.PaymentMethods
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			if (method == null)
				return Result.Failure<PaymentMethod>("Not found");
			return Result.Success(method);
		}

		public async Task<Result> Update(PaymentMethod paymentMethod)
		{
			try
			{
				await _dbContext.PaymentMethods
					.Where(x => x.Id == paymentMethod.Id)
					.ExecuteUpdateAsync(s => s
					.SetProperty(x => x.Title, paymentMethod.Title)
					.SetProperty(x => x.Description, paymentMethod.Description)
					.SetProperty(x => x.UpdatedAt, DateTime.UtcNow));
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> Delete(int paymentMethodId)
		{
			try
			{
				await _dbContext.PaymentMethods
					.Where(x => x.Id == paymentMethodId)
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
