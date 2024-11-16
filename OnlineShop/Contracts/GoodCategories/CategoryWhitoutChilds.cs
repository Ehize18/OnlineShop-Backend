namespace OnlineShop.Contracts.GoodCategories
{
	public record CategoryWhitoutChilds(int id, string title, string description, int parentId, DateTime createdAt, DateTime updatedAt);
}
