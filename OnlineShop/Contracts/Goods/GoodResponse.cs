namespace OnlineShop.Contracts.Goods
{
	public record GoodResponse(int id, string name, string description, int price, int categoryId,
		List<int?> imageIds, int count,
		DateTime createdAt, DateTime updatedAt);
}
