namespace OnlineShop.Contracts.Goods
{
	public record GoodRequest(string name, string description, int price, int categoryId, int count);
}
