namespace OnlineShop.Contracts.Basket
{
	public record BasketResponse(int count, int totalPrice, List<BasketItemResponse>? items);
}
