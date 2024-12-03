using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;
using OnlineShop.Core.Interfaces.Repositories;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public class GoodCategoriesRepository : IGoodCategoriesRepository
	{
		public readonly OnlineStoreDbContext _dbContext;

		public GoodCategoriesRepository(OnlineStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result> AddCategory(GoodCategory category)
		{
			try
			{
				await _dbContext.AddAsync(category);
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result<GoodCategory>> GetCategoryById(int id)
		{
			var category = await _dbContext.GoodCategories
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);
			if (category == null)
				return Result.Failure<GoodCategory>("Category not found");
			return Result.Success(category);
		}

		public async Task<Result<List<GoodCategory>>> GetAllCategories()
		{
			var categories = await _dbContext.GoodCategories
				.AsNoTracking()
				.OrderBy(x => x.Title)
				.ToListAsync();
			var result = CategoriesTreeBuilder.CreateAllTrees(categories);
			return Result.Success(result);
		}

		public async Task<Result<List<GoodCategory>>> GetCategoriesByParrentId(int? id)
		{
			var categories = await _dbContext.GoodCategories
				.AsNoTracking()
				.Where(x => x.ParentId == id)
				.OrderBy(x => x.Title)
				.ToListAsync();
			return Result.Success(categories);
		}

		public async Task<Result> Update(int id, GoodCategory category)
		{
			try
			{
				await _dbContext.GoodCategories
				.Where(x => x.Id == id)
				.ExecuteUpdateAsync(s => s
				.SetProperty(x => x.ParentId, x => category.ParentId)
				.SetProperty(x => x.Title, x => category.Title)
				.SetProperty(x => x.Description, x => category.Description)
				.SetProperty(x => x.UpdatedAt, x => DateTime.UtcNow));
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> DeleteCascade(int id)
		{
			try
			{
				await _dbContext.GoodCategories
				.AsNoTracking()
				.Where(x => x.Id == id)
				.ExecuteDeleteAsync();
				await _dbContext.GoodCategories
					.AsNoTracking()
					.Where(x => x.ParentId == id)
					.ExecuteDeleteAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> DeleteWithNewParentId(int id, int? newParentId)
		{
			try
			{
				await _dbContext.GoodCategories
				.AsNoTracking()
				.Where(x => x.Id == id)
				.ExecuteDeleteAsync();
				await _dbContext.GoodCategories
					.AsNoTracking()
					.Where(x => x.ParentId == id)
					.ExecuteUpdateAsync(s => s
					.SetProperty(x => x.ParentId, x => newParentId)
					.SetProperty(x => x.UpdatedAt, x => DateTime.UtcNow));
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
	}
}
