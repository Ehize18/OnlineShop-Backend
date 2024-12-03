namespace OnlineShop.DataBase.PostgreSQL.Entities
{
	public class ImageEntity
	{
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public int GoodId { get; set; }
		public GoodEntity Good { get; set; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }
	}
}
