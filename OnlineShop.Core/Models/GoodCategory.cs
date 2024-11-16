namespace OnlineShop.Core.Models
{
	public class GoodCategory
	{
		public int? Id { get; }
		public string Title { get; }
		public string Description { get; }
		public int? ParentId { get; }
		public List<GoodCategory> Childs { get; } = new();
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public GoodCategory() { }

		public GoodCategory(string title, string description, int? parentId)
		{
			Title = title;
			Description = description;
			ParentId = parentId;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}
		public GoodCategory(string title, string description, int? parentId, DateTime createdAt, DateTime updatedAt)
		{
			Title = title;
			Description = description;
			ParentId = parentId;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
		public GoodCategory(string title, string description)
		{
			Title = title;
			Description = description;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public GoodCategory(string title, string description, DateTime createdAt, DateTime updatedAt)
		{
			Title = title;
			Description = description;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public GoodCategory(int? id, string title, string description, int? parentId, DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Title = title;
			Description = description;
			ParentId = parentId;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public void AddChild(GoodCategory child)
		{
			if (!this.Childs.Contains(child))
				this.Childs.Add(child);
		}

		public void AddChilds(IEnumerable<GoodCategory> childs)
		{
			foreach (var child in childs)
				AddChild(child);
		}
	}

	public static class CategoriesTreeBuilder
	{
		public static List<GoodCategory> CreateAllTrees(List<GoodCategory> goodCategories)
		{
			var categoriesWithoutChilds = goodCategories.Where(c => c.ParentId == null).ToList();
			foreach (var category in categoriesWithoutChilds)
			{
				goodCategories.Remove(category);
				var childs = goodCategories.Where(c => c.ParentId == category.Id).ToList();
				category.AddChilds(childs);
			}
			return categoriesWithoutChilds;
		}
	}
}
