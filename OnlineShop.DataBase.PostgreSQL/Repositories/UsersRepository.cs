using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;
using OnlineShop.Core.Interfaces.Repositories;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly OnlineStoreDbContext _dbContext;

		public UsersRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result<User>> GetUserByEmail(string email)
		{
			var user = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Email == email);
			if (user == null)
				return Result.Failure<User>("Пользователь не найден");
			return Result.Success(user);
		}

		public async Task<Result<User>> GetUserByRefreshToken(string token)
		{
			var user = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.RefreshToken == token);
			if (user == null)
				return Result.Failure<User>("Пользователь не найден");
			return Result.Success(user);
		}

		public async Task<Result> AddUser(User user)
		{
			try
			{
				await _dbContext.AddAsync(user);
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch
			{
				return Result.Failure("Пользователь с такими данными уже существует");
			}
		}

		public async Task<Result> UpdateRefreshToken(int id, string refreshToken)
		{
			
			try
			{
				await _dbContext.Users
				.AsNoTracking()
				.Where(x => x.Id == id)
				.ExecuteUpdateAsync(s => s
				.SetProperty(x => x.RefreshToken, x => refreshToken));
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
	}
}
