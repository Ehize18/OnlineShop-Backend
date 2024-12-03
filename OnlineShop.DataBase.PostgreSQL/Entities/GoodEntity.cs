using OnlineShop.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.DataBase.PostgreSQL.Entities
{
	public class GoodEntity
	{
		[Key, ForeignKey("Good")]
		public int? Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public int CategoryId { get; set; }
		public GoodCategoryEntity Category { get; set; }
		public List<ImageEntity> Images {  get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
