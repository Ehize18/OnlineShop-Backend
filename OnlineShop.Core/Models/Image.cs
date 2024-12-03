namespace OnlineShop.Core.Models
{
	public class Image
	{
		public int? Id { get; }
		public string Name { get; }
		public string Path { get; }
		public int GoodId { get; }
		public Good Good { get; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public Image() { }

		public Image(int? id, string name, string path, int goodId, Good good)
		{
			Id = id;
			Name = name;
			Path = path;
			GoodId = goodId;
			Good = good;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public Image(int? id, string name, string path, Good good)
		{
			Id = id;
			Name = name;
			Path = path;
			GoodId = (int)good.Id;
			Good = good;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public Image(int? id, string name, string path, int goodId, Good good,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Path = path;
			GoodId = goodId;
			Good = good;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public Image(int? id, string name, string path, Good good,
			DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Name = name;
			Path = path;
			GoodId = (int)good.Id;
			Good = good;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
	}
}
