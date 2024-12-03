using OnlineShop.Core.Models;

namespace OnlineShop.DataBase.PostgreSQL.Entities
{
	public class GoodCategoryEntity
	{
		public int? Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int? ParentId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public List<GoodEntity> Goods { get; set; }
	}
}
