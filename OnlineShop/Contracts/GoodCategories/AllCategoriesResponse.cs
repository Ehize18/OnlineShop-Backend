using OnlineShop.Core.Models;

namespace OnlineShop.Contracts.GoodCategories
{
	public record AllCategoriesResponse(int count, List<GoodCategory> GoodCategories);
}
