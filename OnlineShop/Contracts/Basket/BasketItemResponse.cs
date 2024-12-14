namespace OnlineShop.Contracts.Basket
{
	public record BasketItemResponse(int id, string name, string description, int price, int count);
}
