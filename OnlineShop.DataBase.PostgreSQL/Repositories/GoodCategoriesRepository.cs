using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Models;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.DataBase.PostgreSQL.Entities;

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
			var entity = MapToEntity(category);
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

		public async Task<Result<GoodCategory>> GetCategoryById(int id)
		{
			var categoryEntity = await _dbContext.GoodCategories
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.Id == id);
			if (categoryEntity == null)
				return Result.Failure<GoodCategory>("Category not found");
			var result = MapEntity(categoryEntity);
			return Result.Success(result);
		}

		public async Task<Result<List<GoodCategory>>> GetAllCategories()
		{
			var categoriesEntities = await _dbContext.GoodCategories
				.AsNoTracking()
				.OrderBy(x => x.Title)
				.ToListAsync();
			var categories = new List<GoodCategory>();
			foreach (var entity in categoriesEntities)
			{
				categories.Add(new GoodCategory(entity.Id, entity.Title, entity.Description, entity.ParentId, entity.CreatedAt, entity.UpdatedAt));
			}
			var result = CategoriesTreeBuilder.CreateAllTrees(categories);
			return Result.Success(result);
		}

		public async Task<Result<List<GoodCategory>>> GetCategoriesByParrentId(int? id)
		{
			var canegoriesEntities = await _dbContext.GoodCategories
				.AsNoTracking()
				.Where(x => x.ParentId == id)
				.OrderBy(x => x.Title)
				.ToListAsync();
			var result = MapEntities(canegoriesEntities);
			return Result.Success(result);
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

		private GoodCategory MapEntity(GoodCategoryEntity entity)
		{
			return new GoodCategory(entity.Id, entity.Title, entity.Description, entity.ParentId, entity.CreatedAt, entity.UpdatedAt);
		}

		private List<GoodCategory> MapEntities(List<GoodCategoryEntity> entities)
		{
			var categories = new List<GoodCategory>();
			foreach (var entity in entities)
			{
				categories.Add(new GoodCategory(entity.Id, entity.Title, entity.Description, entity.ParentId, entity.CreatedAt, entity.UpdatedAt));
			}
			return categories;
		}

		private GoodCategoryEntity MapToEntity(GoodCategory category)
		{
			return new GoodCategoryEntity()
			{
				Id = category.Id,
				Title = category.Title,
				Description = category.Description,
				ParentId = category.ParentId,
				CreatedAt = category.CreatedAt,
				UpdatedAt = category.UpdatedAt
			};
		}
	}
}
