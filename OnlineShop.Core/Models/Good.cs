using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Models
{
	public class Good
	{
		public int? Id { get; }
		public string Name { get; }
		public string Description { get; }
		public int Price { get; }
		public int CategoryId { get; }
		public GoodCategory Category { get; }
		public List<Image> Images { get; }
		public int Count { get; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public Good() { }

		public Good(int? id, string name, string description, int price, int categoryId, GoodCategory category)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = categoryId;
			Category = category;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public Good(int? id, string name, string description, int price, GoodCategory category, int count)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = (int)category.Id;
			//Category = category;
			Count = count;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public Good(int? id, string name, string description, int price, int categoryId, GoodCategory category,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = categoryId;
			Category = category;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public Good(int? id, string name, string description, int price, int categoryId, int count,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = categoryId;
			Count = count;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public Good(int? id, string name, string description, int price, GoodCategory category,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = (int)category.Id;
			Category = category;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public Good(int? id, string name, string description, int price, int categoryId, GoodCategory category, List<Image> images,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			CategoryId = categoryId;
			Category = category;
			Images = images;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
	}
}
