using OnlineShop.Contracts.Goods;

namespace OnlineShop.Contracts.GoodCategories
{
	public record GoodCategoryGoodsResponse(int count, List<GoodResponse> Items);
}
