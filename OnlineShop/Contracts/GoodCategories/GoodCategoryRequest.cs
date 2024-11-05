using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Contracts.GoodCategories
{
	public record GoodCategoryRequest([Required]string title, [Required]string description, int? parentId);
}
