namespace OnlineShop.Core.Models
{
	public class GoodCategory
	{
		public int? Id { get; }
		public string Title { get; }
		public string Description { get; }
		public int? ParentId { get; }
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
	}
}
