using OnlineShop.Core.Models;
using OnlineShop.DataBase.PostgreSQL.Entities;

namespace OnlineShop.DataBase.PostgreSQL.Repositories
{
	public static class Mapper
	{
		public static GoodCategory MapEntity(GoodCategoryEntity entity)
		{
			return new GoodCategory(entity.Id, entity.Title, entity.Description, entity.ParentId, entity.CreatedAt, entity.UpdatedAt);
		}

		public static List<GoodCategory> MapEntities(List<GoodCategoryEntity> entities)
		{
			var categories = new List<GoodCategory>();
			foreach (var entity in entities)
			{
				categories.Add(new GoodCategory(entity.Id, entity.Title, entity.Description, entity.ParentId, entity.CreatedAt, entity.UpdatedAt));
			}
			return categories;
		}

		public static GoodCategoryEntity MapToEntity(GoodCategory category)
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
		public static GoodEntity MapToEntity(Good good)
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
		public static Good MapEntity(GoodEntity goodEntity)
		{
			var category = goodEntity.Category == null ? null : MapEntity(goodEntity.Category);
			return new Good(goodEntity.Id, goodEntity.Name, goodEntity.Description, goodEntity.Price, goodEntity.CategoryId, category);
		}

		public static Image MapEntity(ImageEntity entity)
		{
			var good = entity.Good == null ? null : MapEntity(entity.Good);
			return new Image(entity.Id, entity.Name, entity.Path, good, entity.CreatedAt, entity.UpdatedAt);
		}
	}
}
